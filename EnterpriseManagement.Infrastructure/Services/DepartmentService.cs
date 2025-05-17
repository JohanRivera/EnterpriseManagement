using EnterpriseManagement.Core.Entities.General;
using EnterpriseManagement.Core.Interfaces.IRepositories;
using EnterpriseManagement.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseManagement.Infrastructure.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _unitOfWork.Departments.GetAllAsync();
        }

        public async Task<Department> GetByIdAsync(int id) => await _unitOfWork.Departments.GetByIdAsync(id);

        public async Task AddAsync(Department department)
        {
            await _unitOfWork.Departments.AddAsync(department);
            await _unitOfWork.CompleteAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _unitOfWork.Departments.Update(department);
            await _unitOfWork.CompleteAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department != null)
            {
                _unitOfWork.Departments.Delete(department);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null)
                return Enumerable.Empty<Employee>();

            return await _unitOfWork.Employees.GetByDepartmentIdAsync(id);
        }

    }
}
