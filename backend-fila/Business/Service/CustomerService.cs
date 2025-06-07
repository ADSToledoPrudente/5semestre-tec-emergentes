using backend_fila.Business.Interface;
using backend_fila.Data;
using backend_fila.Data.DTO.Customer;
using backend_fila.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_fila.Business.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly Context _context;

        public CustomerService(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddToQueue(int queueId, CustomerDTO customerDto)
        {
            try
            {
                var queue = await _context.Queue.Where(w => w.Id == queueId).FirstOrDefaultAsync();
                if (queue is null) { throw new Exception($"Queue [{queueId}] not found."); }

                var customer = new Customer()
                {
                    Active = true,
                    Date = DateTime.Now,
                    Name = customerDto.Name,
                    Priority = customerDto.Priority ?? false,
                    QueueId = queueId
                };

                _context.Customer.Add(customer);

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            { throw; }
        }

        public async Task RemoveFromQueue(int customerId)
        {
            try
            {
                var customer = await _context.Customer.Where(w => w.Id == customerId).FirstOrDefaultAsync();
                if(customer is null) { throw new Exception($"Customer [{customerId}] not found."); }

                customer.Active = false;

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            { throw; }
        }
    }
}
