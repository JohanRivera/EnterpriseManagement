using EnterpriseManagement.Core.Entities.General;
using EnterpriseManagement.Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Obtiene todos los departamentos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            return OkResult(departments);
        }

        /// <summary>
        /// Obtiene un departamento por ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var department = await _departmentService.GetByIdAsync(id);
            if (department == null)
                return NotFound();

            return OkResult(department);
        }

        /// <summary>
        /// Crea un nuevo departamento.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Department department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _departmentService.AddAsync(department);
            return CreatedAtAction(nameof(GetById), new { id = department.Id }, department);

        }

        /// <summary>
        /// Actualiza un departamento existente.
        /// </summary>
        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, Department updated)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != updated.Id)
                return BadRequest("Id mismatch");

            await _departmentService.UpdateAsync(updated);
            return OkResult(new { message = "Department Update successfully" });
        }

        /// <summary>
        /// Elimina un departamento por ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.DeleteAsync(id);
            return OkResult(new { message = "Department deleted successfully" });
        }

        /// <summary>
        /// Devuelve una lista de empleados por departamento ID.
        /// </summary>
        [HttpGet("{id}/employees")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeesByDepartment(int id)
        {
            var employees = await _departmentService.GetByDepartmentIdAsync(id);
            if (employees == null)
                return BadRequest("Id mismatch");
            return OkResult(employees);
        }
    }
}
