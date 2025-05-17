using EnterpriseManagement.Core.Entities.General;
using EnterpriseManagement.Core.Interfaces.IRepositories;
using EnterpriseManagement.Core.Interfaces.IServices;

namespace EnterpriseManagement.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _unitOfWork.Employees.GetAllAsync();
        }

        public async Task<Employee> GetByIdAsync(int id) => await _unitOfWork.Employees.GetByIdAsync(id);

        public async Task AddAsync(Employee employee)
        {
            await _unitOfWork.Employees.AddAsync(employee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            _unitOfWork.Employees.Update(employee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee != null)
            {
                _unitOfWork.Employees.Delete(employee);
                await _unitOfWork.CompleteAsync();
            }
        }
    }
}
