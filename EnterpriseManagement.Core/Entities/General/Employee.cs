using EnterpriseManagement.Core.Entities.Enumarators;

namespace EnterpriseManagement.Core.Entities.General
{
    public class Employee : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public JobPosition Position { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }


        public decimal CalculateSalary()
        {
            return Position switch
            {
                JobPosition.Developer => Salary * 1.10m, // 10% bono
                JobPosition.Manager => Salary * 1.20m,   // 20% bono
                _ => Salary                              // HR y Sales: fijo
            };
        }
    }
}
