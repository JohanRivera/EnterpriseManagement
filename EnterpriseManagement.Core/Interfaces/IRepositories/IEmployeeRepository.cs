using EnterpriseManagement.Core.Entities.General;

namespace EnterpriseManagement.Core.Interfaces.IRepositories
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId);
    }
}
