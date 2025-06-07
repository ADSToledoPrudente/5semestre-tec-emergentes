using Microsoft.AspNetCore.Mvc;

namespace backend_fila.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BaseController : ControllerBase
    {
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Success(object? data = null, string message = "Requisição completa com sucesso.")
        {
            return new OkObjectResult(new BaseResponse
            {
                Data = data,
                Message = message,
                Success = true
            });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Error(string message = "Requisição falhou!")
        {
            return new BadRequestObjectResult(new BaseResponse
            {
                Data = null,
                Message = message,
                Success = false
            });
        }
    }

    public class BaseResponse
    {
        public object? Data { get; set; } = new object();
        public string Message { get; set; } = string.Empty;
        public bool Success { get; set; }
    }
}
