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

    }
}
