using EnterpriseManagement.Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;

        protected BaseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // Se agregan helpers generales para los controladores

        protected IActionResult OkResult(object data)
        {
            return Ok(new { success = true, data });
        }

        protected IActionResult NotFoundResult(string entity, object id)
        {
            return NotFound(new { success = false, message = $"{entity} with id '{id}' was not found." });
        }

        protected IActionResult CreatedResult(string routeName, object routeValues, object data)
        {
            return CreatedAtAction(routeName, routeValues, new { success = true, data });
        }

        protected IActionResult ErrorResult(string message)
        {
            return BadRequest(new { success = false, message });
        }
    }
}
