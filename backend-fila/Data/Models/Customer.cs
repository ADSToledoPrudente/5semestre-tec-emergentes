using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_fila.Data.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Priority { get; set; }
        public DateTime Date { get; set; }
        public bool Active { get; set; }
        public int QueueId { get; set; }
        public virtual Queue? Queue { get; set; }
    }
}
