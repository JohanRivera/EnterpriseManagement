namespace EnterpriseManagement.Core.Entities.General
{
    public class Department : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
