using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend_fila.Data.Models
{
    public class Queue
    {
        public Queue()
        {
            Customer = new HashSet<Customer>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
        public virtual ICollection<Customer> Customer { get; set; }
    }
}
