using EnterpriseManagement.Core.Entities.General;
using EnterpriseManagement.Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.API.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : BaseController
    {
        public DepartmentController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        /// <summary>
        /// Obtiene todos los departamentos.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
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
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null) return NotFoundResult(nameof(Department), id);
            return OkResult(department);
        }

        /// <summary>
        /// Crea un nuevo departamento.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(Department department)
        {
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.CompleteAsync();
            return CreatedResult(nameof(GetById), new { id = department.Id }, department);
        }

        /// <summary>
        /// Actualiza un departamento existente.
        /// </summary>
        [HttpPut("{id}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(int id, Department updated)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null) return NotFoundResult(nameof(Department), id);

            department.Name = updated.Name;
            _unitOfWork.Departments.Update(department);
            await _unitOfWork.CompleteAsync();

            return OkResult(department);
        }

        /// <summary>
        /// Elimina un departamento por ID.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null) return NotFoundResult(nameof(Department), id);

            _unitOfWork.Departments.Update(department);
            await _unitOfWork.CompleteAsync();

            return OkResult(new { message = "Department deleted successfully" });
        }

        /// <summary>
        /// Devuelve una lista de empleados por departamento ID.
        /// </summary>
        [HttpGet("{id}/employees")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmployeesByDepartment(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null) return NotFoundResult(nameof(Department), id);

            var employees = await _unitOfWork.Employees.GetByDepartmentIdAsync(id);
            return OkResult(employees);
        }
    }
}
