using EnterpriseManagement.Core.Entities.Enumarators;
using EnterpriseManagement.Core.Entities.General;
using EnterpriseManagement.Core.Interfaces.IRepositories;
using EnterpriseManagement.Infrastructure.Services;
using Moq;

namespace EnterpriseManagement.UnitTest.API.Tests
{
    public class EmployeeServiceTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IEmployeeRepository> _mockEmployeeRepo;
        private readonly EmployeeService _service;

        public EmployeeServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockEmployeeRepo = new Mock<IEmployeeRepository>();

            // Setup para que el UnitOfWork devuelva el mock del repositorio de Employees
            _mockUnitOfWork.Setup(u => u.Employees).Returns(_mockEmployeeRepo.Object);

            _service = new EmployeeService(_mockUnitOfWork.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe", Position = JobPosition.Developer },
                new Employee { Id = 2, Name = "Jane Smith", Position = JobPosition.Manager }
            };
            _mockEmployeeRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(employees);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Contains(result, e => e.Name == "John Doe");
            Assert.Contains(result, e => e.Name == "Jane Smith");
        }

        [Fact]
        public async Task GetByIdAsync_ExistingId_ReturnsEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe", Position = JobPosition.Developer };
            _mockEmployeeRepo.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(employee);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John Doe", result.Name);
        }

        [Fact]
        public async Task AddAsync_AddsEmployeeAndCompletesUnitOfWork()
        {
            // Arrange
            var employee = new Employee { Id = 3, Name = "Alice Brown", Position = JobPosition.HR };
            _mockEmployeeRepo.Setup(repo => repo.AddAsync(employee)).Returns(Task.CompletedTask);
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            await _service.AddAsync(employee);

            // Assert
            _mockEmployeeRepo.Verify(repo => repo.AddAsync(employee), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesEmployeeAndCompletesUnitOfWork()
        {
            // Arrange
            var employee = new Employee { Id = 4, Name = "Bob White", Position = JobPosition.HR };
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            await _service.UpdateAsync(employee);

            // Assert
            _mockEmployeeRepo.Verify(repo => repo.Update(employee), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ExistingEmployee_DeletesAndCompletes()
        {
            // Arrange
            var employee = new Employee { Id = 5, Name = "Eve Black", Position = JobPosition.Developer };
            _mockEmployeeRepo.Setup(repo => repo.GetByIdAsync(5)).ReturnsAsync(employee);
            _mockEmployeeRepo.Setup(repo => repo.Delete(employee));
            _mockUnitOfWork.Setup(u => u.CompleteAsync()).ReturnsAsync(1);

            // Act
            await _service.DeleteAsync(5);

            // Assert
            _mockEmployeeRepo.Verify(repo => repo.Delete(employee), Times.Once);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_NonExistingEmployee_DoesNotDeleteOrComplete()
        {
            // Arrange
            _mockEmployeeRepo.Setup(repo => repo.GetByIdAsync(10)).ReturnsAsync((Employee?)null);

            // Act
            await _service.DeleteAsync(10);

            // Assert
            _mockEmployeeRepo.Verify(repo => repo.Delete(It.IsAny<Employee>()), Times.Never);
            _mockUnitOfWork.Verify(u => u.CompleteAsync(), Times.Never);
        }
    }
}
