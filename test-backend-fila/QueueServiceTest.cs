using Moq;
using Microsoft.EntityFrameworkCore;
using backend_fila.Data;
using backend_fila.Business.Service;
using backend_fila.Data.Models;

namespace test_backend_fila
{
    public class QueueServiceTest
    {
        [Fact(DisplayName = "Should create a new queue")]
        public async Task New()
        {
            var mockSet = new Mock<DbSet<Queue>>();

            var mockContext = new Mock<Context>();
            mockContext.Setup(m => m.Queue).Returns(mockSet.Object);
            mockContext.Setup(m => m.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            var service = new QueueService(mockContext.Object);

            await service.New("NewQueue");

            mockSet.Verify(m => m.Add(It.Is<Queue>(q => q.Name == "NewQueue" && q.Active)), Times.Once);
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact(DisplayName = "Should end a queue")]
        public async Task End()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);
            var queue = new Queue { Id = 1, Name = "Test Queue", Active = true };
            context.Queue.Add(queue);
            await context.SaveChangesAsync();

            var service = new QueueService(context);

            await service.End(1);

            var updatedQueue = await context.Queue.FindAsync(1);
            Assert.NotNull(updatedQueue);
            Assert.False(updatedQueue.Active);
        }

        [Fact(DisplayName = "Should throw a exception when queue not found")]
        public async Task EndException()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);

            var service = new QueueService(context);

            var exception = await Assert.ThrowsAsync<Exception>(() => service.End(1));

            Assert.Equal("Queue [1] not found.", exception.Message);
        }

        [Fact(DisplayName = "Should return all active queues")]
        public async Task GetAll()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);

            var queue1 = new Queue { Id = 1, Name = "Queue 1", Active = true };
            var queue2 = new Queue { Id = 2, Name = "Queue 2", Active = false };
            var queue3 = new Queue { Id = 3, Name = "Queue 3", Active = true };

            context.Queue.AddRange(queue1, queue2, queue3);
            await context.SaveChangesAsync();

            var service = new QueueService(context);

            var result = await service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Contains(result, q => q.Id == 1);
            Assert.Contains(result, q => q.Id == 3);
            Assert.DoesNotContain(result, q => q.Id == 2);
        }

        [Fact(DisplayName = "Should return queue and active customers")]
        public async Task GetById()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);

            var queue = new Queue { Id = 1, Name = "Queue 1", Active = true };
            var customer1 = new Customer { Id = 1, Name = "Customer 1", Active = true, Priority = true, Date = DateTime.Now };
            var customer2 = new Customer { Id = 2, Name = "Customer 2", Active = false, Priority = false, Date = DateTime.Now.AddDays(-1) };
            var customer3 = new Customer { Id = 3, Name = "Customer 3", Active = true, Priority = false, Date = DateTime.Now.AddDays(-2) };

            queue.Customer.Add(customer1);
            queue.Customer.Add(customer2);
            queue.Customer.Add(customer3);

            context.Queue.Add(queue);
            await context.SaveChangesAsync();

            var service = new QueueService(context);

            var result = await service.Get(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Queue 1", result.Name);
            Assert.NotNull(result.Customer);
            Assert.Equal(2, result.Customer.Count);
            Assert.Contains(result.Customer, c => c.Id == 1);
            Assert.Contains(result.Customer, c => c.Id == 3);
            Assert.DoesNotContain(result.Customer, c => c.Id == 2);
            Assert.Equal(1, result.Customer.ToList()[0].Id);
            Assert.Equal(3, result.Customer.ToList()[1].Id);
        }
    }
}
