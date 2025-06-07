using backend_fila.Business.Service;
using backend_fila.Data.DTO.Customer;
using backend_fila.Data;
using Microsoft.EntityFrameworkCore;
using backend_fila.Data.Models;

namespace test_backend_fila
{
    public class CustomerServiceTest
    {
        [Fact(DisplayName = "Should add customer to existing queue")]
        public async Task AddToQueue()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);

            var queue = new Queue { Id = 1, Name = "Test Queue", Active = true };
            context.Queue.Add(queue);
            await context.SaveChangesAsync();

            var service = new CustomerService(context);

            var customerDto = new CustomerDTO
            {
                Name = "Customer 1",
                Priority = true
            };

            await service.AddToQueue(1, customerDto);

            var customers = await context.Customer.ToListAsync();
            Assert.Single(customers);
            var addedCustomer = customers.First();
            Assert.Equal("Customer 1", addedCustomer.Name);
            Assert.True(addedCustomer.Active);
            Assert.True(addedCustomer.Priority);
            Assert.Equal(1, addedCustomer.QueueId);
        }

        [Fact(DisplayName = "Should throw exception when queue not found in AddToQueue")]
        public async Task AddToQueueException()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);
            var service = new CustomerService(context);

            var customerDto = new CustomerDTO { Name = "Customer X", Priority = false };

            var ex = await Assert.ThrowsAsync<Exception>(() => service.AddToQueue(99, customerDto));
            Assert.Equal("Queue [99] not found.", ex.Message);
        }

        [Fact(DisplayName = "Should deactivate customer when removed from queue")]
        public async Task RemoveFromQueue()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);

            var customer = new Customer
            {
                Id = 1,
                Name = "Customer A",
                Active = true,
                Date = DateTime.Now,
                Priority = false,
                QueueId = 1
            };

            context.Customer.Add(customer);
            await context.SaveChangesAsync();

            var service = new CustomerService(context);

            await service.RemoveFromQueue(1);

            var updatedCustomer = await context.Customer.FindAsync(1);
            Assert.NotNull(updatedCustomer);
            Assert.False(updatedCustomer.Active);
        }

        [Fact(DisplayName = "Should throw exception when customer not found in RemoveFromQueue")]
        public async Task RemoveFromQueueException()
        {
            var options = new DbContextOptionsBuilder<Context>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using var context = new Context(options);
            var service = new CustomerService(context);

            var ex = await Assert.ThrowsAsync<Exception>(() => service.RemoveFromQueue(999));
            Assert.Equal("Customer [999] not found.", ex.Message);
        }

    }
}
