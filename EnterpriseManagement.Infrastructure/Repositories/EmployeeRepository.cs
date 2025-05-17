using EnterpriseManagement.Core.Entities.General;
using EnterpriseManagement.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DbContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            return await _context.Set<Employee>()
                .Where(e => e.DepartmentId == departmentId)
                .Select(s => new Employee
                {
                    DepartmentId = s.DepartmentId,
                    Name = s.Name,
                    Salary = s.Salary,
                    Email = s.Email,
                    Position = s.Position,
                    Id = s.Id
                }).ToListAsync();
        }
    }
}
