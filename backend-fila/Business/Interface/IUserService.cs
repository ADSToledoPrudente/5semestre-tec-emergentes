using backend_fila.Data.DTO.User;

namespace backend_fila.Business.Interface
{
    public interface IUserService
    {
        Task Login(LoginDTO dto);
    }
}
