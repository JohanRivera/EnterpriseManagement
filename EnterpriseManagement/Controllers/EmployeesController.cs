using EnterpriseManagement.Core.Entities.General;
using EnterpriseManagement.Core.Interfaces.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseManagement.API.Controllers
{
    [Route("api/[controller]")]
    public class EmployeesController : BaseController
    {
        public EmployeesController(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _unitOfWork.Employees.GetAllAsync();
            return OkResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null) return NotFoundResult(nameof(Employee), id);
            return OkResult(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee employee)
        {
            employee.CalculateSalary(); // Usa el método del modelo
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();

            return CreatedResult(nameof(GetById), new { id = employee.Id }, employee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Employee updated)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null) return NotFoundResult(nameof(Employee), id);

            employee.Name = updated.Name;
            employee.Email = updated.Email;
            employee.Position = updated.Position;
            employee.DepartmentId = updated.DepartmentId;
            employee.Salary = updated.Salary;

            employee.CalculateSalary(); // Aplica la regla de negocio encapsulada

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();

            return OkResult(employee);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null) return NotFoundResult(nameof(Employee), id);

            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();

            return OkResult(new { message = "Employee deleted successfully" });
        }
    }
}
