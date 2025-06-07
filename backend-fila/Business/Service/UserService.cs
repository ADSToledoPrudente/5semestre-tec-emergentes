using backend_fila.Business.Interface;
using backend_fila.Data;
using backend_fila.Data.DTO.User;
using Microsoft.EntityFrameworkCore;

namespace backend_fila.Business.Service
{
    public class UserService : IUserService
    {
        private readonly Context _context;

        public UserService(Context context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Login(LoginDTO dto)
        {
            try
            {
                var user = await _context.User.Where(w => w.Email.Equals(dto.Email)).FirstOrDefaultAsync();
                
                if(user is null) { throw new Exception("User not found."); }
                if(user.Password != dto.Password) { throw new Exception("User not found."); }
            }
            catch (Exception)
            { throw; }
        }
    }
}
