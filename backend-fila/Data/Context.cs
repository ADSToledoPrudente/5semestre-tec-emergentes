using backend_fila.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace backend_fila.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public Context() : base() { }

        public virtual DbSet<Queue> Queue { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<User> User { get; set; }
    }
}
