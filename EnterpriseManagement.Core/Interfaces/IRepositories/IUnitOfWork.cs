namespace EnterpriseManagement.Core.Interfaces.IRepositories
{
    public interface IUnitOfWork : IDisposable
    {
        IEmployeeRepository Employees { get; }
        IDepartmentRepository Departments { get; }
        Task<int> CompleteAsync();
    }
}
