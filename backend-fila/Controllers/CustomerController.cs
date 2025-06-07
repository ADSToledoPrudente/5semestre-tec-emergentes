using backend_fila.Business.Interface;
using backend_fila.Data.DTO.Customer;
using Microsoft.AspNetCore.Mvc;

namespace backend_fila.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
        }

        [HttpPost("{queueId}")]
        public async Task<IActionResult> AddToQueue(int queueId, CustomerDTO dto)
        {
            try
            {
                await _customerService.AddToQueue(queueId, dto);
                return Success();
            }       
            catch (Exception ex)
            { return Error(ex.Message); }
        }

        [HttpDelete("{customerId}")]
        public async Task<IActionResult> RemoveFromQueue(int customerId)
        {
            try
            {
                await _customerService.RemoveFromQueue(customerId);
                return Success();
            }
            catch(Exception ex) 
            { return Error(ex.Message); }
        }
    }
}
