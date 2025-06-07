using backend_fila.Business.Interface;
using backend_fila.Data;
using backend_fila.Data.DTO.Customer;
using backend_fila.Data.DTO.Queue;
using backend_fila.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_fila.Business.Service
{
    public class QueueService : IQueueService
    {
        private readonly Context _context;

        public QueueService(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public async Task New(string name)
        {
            try
            {
                var queue = new Queue()
                {
                    Name = name,
                    Active = true
                };

                _context.Queue.Add(queue);

                await _context.SaveChangesAsync();
            }
            catch(Exception)
            { throw; }
        }

        public async Task End(int id)
        {
            try
            {
                var queue = await _context.Queue.Where(w => w.Id == id).FirstOrDefaultAsync();
                if(queue is null) { throw new Exception($"Queue [{id}] not found."); }

                queue.Active = false;

                await _context.SaveChangesAsync();
            }
            catch(Exception)
            { throw; }
        }

        public async Task<ICollection<QueueDTO>> GetAll()
        {
            try
            {
                return await _context.Queue.Where(w => w.Active)
                .Include(i => i.Customer)
                .Select(s => new QueueDTO
                {
                    Name = s.Name,
                    Active = s.Active,
                    Id = s.Id
                }).ToListAsync();
            }
            catch (Exception)
            { throw; }
        }

        public async Task<QueueDTO> Get(int queueId)
        {
            try
            {
                return await _context.Queue.Where(w => w.Id == queueId)
                    .Include(i => i.Customer)
                    .Select(s => new QueueDTO
                    {
                        Name = s.Name,
                        Active = s.Active,
                        Id = s.Id,
                        Customer = s.Customer.Where(w => w.Active).Select(c => new CustomerDTO
                        {
                            Active = c.Active,
                            Id = c.Id,
                            Date = c.Date,
                            Priority = c.Priority,
                            Name = c.Name
                        })
                        .OrderByDescending(o => o.Priority)
                        .ThenBy(t => t.Date)
                        .ToList()
                    }).FirstOrDefaultAsync() ?? new QueueDTO();
            }
            catch (Exception)
            { throw; }
        }
    }
}
