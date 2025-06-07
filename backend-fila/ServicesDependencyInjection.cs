using backend_fila.Business.Interface;
using backend_fila.Business.Service;
using System.Runtime.CompilerServices;

namespace backend_fila
{
    public static class ServicesDependencyInjection
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IQueueService, QueueService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
