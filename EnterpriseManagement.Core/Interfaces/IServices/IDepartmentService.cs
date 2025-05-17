using EnterpriseManagement.Core.Entities.General;

namespace EnterpriseManagement.Core.Interfaces.IServices
{
    public interface IDepartmentService : IBaseService<Department>
    {
        Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int id);
    }
}
