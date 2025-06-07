using backend_fila.Data.DTO.Customer;

namespace backend_fila.Data.DTO.Queue
{
    public class QueueDTO
    {
        public QueueDTO()
        {
            Customer = new HashSet<CustomerDTO>();
        }

        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? Active { get; set; }
        public ICollection<CustomerDTO>? Customer { get; set; }
    }
}
