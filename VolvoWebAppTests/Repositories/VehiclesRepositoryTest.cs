using Microsoft.EntityFrameworkCore;
using VolvoWebApp.Data;
using VolvoWebApp.Enums;
using VolvoWebApp.Models;
using VolvoWebApp.Repositories;

namespace VolvoWebAppTests.Repositories
{
    public class VehiclesRepositoryTest
    {
        private readonly ApplicationDbContext _context;
        private readonly IVehiclesRepository _repository;

        private readonly List<Vehicle> _testData =
        [
            new Vehicle() { ChassisSeries = "ChassisSeriesA", ChassisNumber = 1, Type = VehicleType.Truck, Color = "Red" },
            new Vehicle() { ChassisSeries = "ChassisSeriesB", ChassisNumber = 1, Type = VehicleType.Truck, Color = "Blue" },
            new Vehicle() { ChassisSeries = "ChassisSeriesC", ChassisNumber = 2, Type = VehicleType.Truck, Color = "Green" },
        ];

        public VehiclesRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _context = new ApplicationDbContext(options);
            _repository = new VehiclesRepository(_context);
        }

        [Fact]
        public async void GetAllAsync()
        {
            // Arrange
            Vehicle testData = _testData[0];
            _context.Vehicle.Add(testData);
            _context.SaveChanges();
            // Act
            var result = await _repository.GetAllAsync();
            // Assert
            Assert.NotEmpty(result);
        }

        /*
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(string id);
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<List<Vehicle>> GetByChassisId(string chassisSeries, uint chassisNumber);
         */
    }
}