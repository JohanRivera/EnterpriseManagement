using EnterpriseManagement.Core.Entities.Enumarators;
using EnterpriseManagement.Core.Entities.General;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseManagement.Infrastructure.Persistence
{
    public class DbInitializer
    {
        public static async Task SeedAsync(AppDbContext context)
        {
            // Aplica migraciones pendientes (opcional)
            await context.Database.MigrateAsync();

            if (!context.Departments.Any())
            {
                var departments = new List<Department>
                {
                    new Department { Name = "IT" },
                    new Department { Name = "HR" },
                    new Department { Name = "Sales" }
                };

                context.Departments.AddRange(departments);
                await context.SaveChangesAsync();
            }

            if (!context.Employees.Any())
            {
                var itDept = context.Departments.FirstOrDefault(d => d.Name == "IT");
                var hrDept = context.Departments.FirstOrDefault(d => d.Name == "HR");
                var salesDept = context.Departments.FirstOrDefault(d => d.Name == "Sales");

                var employees = new List<Employee>
                {
                    new Employee
                    {
                        Name = "Carlos Dev",
                        Email = "carlos@empresa.com",
                        Salary = 1000,
                        Position = JobPosition.Developer,
                        DepartmentId = itDept!.Id
                    },
                    new Employee
                    {
                        Name = "Ana Manager",
                        Email = "ana@empresa.com",
                        Salary = 1500,
                        Position = JobPosition.Manager,
                        DepartmentId = itDept!.Id
                    },
                    new Employee
                    {
                        Name = "Lucía RH",
                        Email = "lucia@empresa.com",
                        Salary = 1200,
                        Position = JobPosition.HR,
                        DepartmentId = hrDept!.Id
                    },
                    new Employee
                    {
                        Name = "Pedro Sales",
                        Email = "pedro@empresa.com",
                        Salary = 1100,
                        Position = JobPosition.Sales,
                        DepartmentId = salesDept!.Id
                    },
                    new Employee
                    {
                        Name = "María Dev",
                        Email = "maria@empresa.com",
                        Salary = 1050,
                        Position = JobPosition.Developer,
                        DepartmentId = itDept!.Id
                    }
                };

                context.Employees.AddRange(employees);
                await context.SaveChangesAsync();
            }
        }
    }
}
