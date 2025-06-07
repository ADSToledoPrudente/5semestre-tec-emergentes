using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

namespace backend_fila.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        private IConfiguration _configuration;

        public ContextFactory(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseMySql(_configuration.GetConnectionString("DbContext"), ServerVersion.AutoDetect(_configuration.GetConnectionString("DbContext")));

            return new Context(optionsBuilder.Options);
        }
    }
}
