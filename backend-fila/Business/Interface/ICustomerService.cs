using backend_fila.Data.DTO.Customer;

namespace backend_fila.Business.Interface
{
    public interface ICustomerService
    {
        Task AddToQueue(int queueId, CustomerDTO customerDto);

        Task RemoveFromQueue(int customerId);
    }
}
