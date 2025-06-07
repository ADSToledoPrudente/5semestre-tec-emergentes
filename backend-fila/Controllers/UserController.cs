using backend_fila.Business.Interface;
using backend_fila.Data.DTO.User;
using Microsoft.AspNetCore.Mvc;

namespace backend_fila.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            try
            {
                await _userService.Login(dto);
                return Success();
            }
            catch(Exception ex) 
            { return Error(ex.Message); }
        }
    }
}
