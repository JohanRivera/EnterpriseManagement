using EnterpriseManagement.Core.Entities.General;
using EnterpriseManagement.Core.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseManagement.Infrastructure.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(DbContext context) : base(context) { }

        public async Task<Department?> GetWithEmployeesAsync(int id)
        {
            return await _context.Set<Department>()
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
