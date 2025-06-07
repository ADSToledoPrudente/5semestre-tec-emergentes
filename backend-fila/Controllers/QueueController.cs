using backend_fila.Business.Interface;
using Microsoft.AspNetCore.Mvc;

namespace backend_fila.Controllers
{
    public class QueueController : BaseController
    {
        private readonly IQueueService _queueService;

        public QueueController(IQueueService queueService)
        {
            _queueService = queueService ?? throw new ArgumentNullException(nameof(queueService));
        }

        [HttpPost("{name}")]
        public async Task<IActionResult> New(string name)
        {
            try
            {
                await _queueService.New(name);
                return Success();
            }
            catch(Exception ex) 
            { return Error(ex.Message); }
        }

        [HttpDelete("{queueId}")]
        public async Task<IActionResult> Delete(int queueId)
        {
            try
            {
                await _queueService.End(queueId);
                return Success();
            }
            catch (Exception ex)
            { return Error(ex.Message); }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var dados = await _queueService.GetAll();
                return Success(dados);
            }
            catch (Exception ex)
            { return Error(ex.Message); }
        }

        [HttpGet("{queueId}")]
        public async Task<IActionResult> GetById(int queueId)
        {
            try
            {
                var dados = await _queueService.Get(queueId);
                return Success(dados);
            }
            catch (Exception ex)
            { return Error(ex.Message); }
        }
    }
}
