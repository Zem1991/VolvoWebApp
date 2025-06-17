using Microsoft.EntityFrameworkCore;
using VolvoWebApp.Data;
using VolvoWebApp.Data.Entities;
using VolvoWebApp.Enums;
using VolvoWebApp.Repositories;

namespace VolvoWebAppTests.Repositories
{
    public class VehiclesRepositoryTest
    {
        private readonly ApplicationDbContext _mockContext;
        private readonly IVehiclesRepository _repository;

        private readonly List<Vehicle> _testEntities =
        [
            new() { ChassisSeries = "ChassisSeriesA", ChassisNumber = 1, Type = VehicleType.Bus, Color = "Red" },
            new() { ChassisSeries = "ChassisSeriesB", ChassisNumber = 1, Type = VehicleType.Car, Color = "Blue" },
            new() { ChassisSeries = "ChassisSeriesC", ChassisNumber = 2, Type = VehicleType.Truck, Color = "Green" },
        ];

        public VehiclesRepositoryTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDb")
                .Options;
            _mockContext = new ApplicationDbContext(options);
            _repository = new VehiclesRepository(_mockContext);
        }

        [Fact]
        public async void GetAllAsync()
        {
            // Arrange
            var entities = _testEntities;
            _mockContext.Vehicle.AddRange(entities);
            _mockContext.SaveChanges();
            // Act
            var result = await _repository.GetAllAsync();
            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            _mockContext.Vehicle.Add(entity);
            _mockContext.SaveChanges();
            // Act
            var result = await _repository.GetByIdAsync(entity.Id);
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void InsertAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            // Act
            var success = await _repository.InsertAsync(entity);
            var record = _mockContext.Vehicle.FirstOrDefault(x => x.Id == entity.Id);
            // Assert
            Assert.True(success);
            Assert.Equal(entity.Id, record?.Id);
        }

        [Fact]
        public async void UpdateAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            _mockContext.Vehicle.Add(entity);
            _mockContext.SaveChanges();
            string newColor = "Yellow";
            // Act
            entity.Color = newColor;
            var success = await _repository.UpdateAsync(entity);
            // Assert
            Assert.True(success);
            Assert.Equal(newColor, entity?.Color);
        }

        [Fact]
        public async void DeleteAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            _mockContext.Vehicle.Add(entity);
            _mockContext.SaveChanges();
            // Act
            var success = await _repository.DeleteAsync(entity);
            var record = _mockContext.Vehicle.FirstOrDefault(x => x.Id == entity.Id);
            // Assert
            Assert.True(success);
            Assert.Null(record);
        }

        [Fact]
        public async void GetByChassisId()
        {
            // Arrange
            string cSeries = "TestSeries";
            uint cNumber = 7;
            var entity = _testEntities[0];
            entity.ChassisSeries = cSeries;
            entity.ChassisNumber = cNumber;
            _mockContext.Vehicle.Add(entity);
            _mockContext.SaveChanges();
            // Act
            var result = await _repository.GetByChassisId(cSeries, cNumber);
            var firstResult = result.FirstOrDefault();
            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(cSeries, firstResult?.ChassisSeries);
            Assert.Equal(cNumber, firstResult?.ChassisNumber);
        }
    }
}