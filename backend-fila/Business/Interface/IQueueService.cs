using backend_fila.Data.DTO.Queue;

namespace backend_fila.Business.Interface
{
    public interface IQueueService
    {
        Task New(string name);

        Task End(int id);

        Task<ICollection<QueueDTO>> GetAll();

        Task<QueueDTO> Get(int queueId);
    }
}
