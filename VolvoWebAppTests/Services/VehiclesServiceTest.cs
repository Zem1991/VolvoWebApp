using AutoMapper;
using Moq;
using VolvoWebApp.Configurations;
using VolvoWebApp.Data.Entities;
using VolvoWebApp.Dtos;
using VolvoWebApp.Enums;
using VolvoWebApp.Repositories;
using VolvoWebApp.Services;

namespace VolvoWebAppTests.Services
{
    public class VehiclesServiceTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IVehiclesRepository> _mockRepository;
        private readonly IVehiclesService _service;

        private readonly List<Vehicle> _testEntities =
        [
            new() { ChassisSeries = "ChassisSeriesA", ChassisNumber = 1, Type = VehicleType.Bus, Color = "Red" },
            new() { ChassisSeries = "ChassisSeriesB", ChassisNumber = 1, Type = VehicleType.Car, Color = "Blue" },
            new() { ChassisSeries = "ChassisSeriesC", ChassisNumber = 2, Type = VehicleType.Truck, Color = "Green" },
        ];

        public VehiclesServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VolvoProfile());
            });
            _mapper = config.CreateMapper();
            _mockRepository = new Mock<IVehiclesRepository>();
            _service = new VehiclesService(_mapper, _mockRepository.Object);
        }

        [Fact]
        public async void GetAllAsync()
        {
            // Arrange
            var entities = _testEntities;
            _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);
            // Act
            var result = await _service.GetAllAsync();
            // Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            _mockRepository.Setup(r => r.GetByIdAsync(entity.Id)).ReturnsAsync(entity);
            // Act
            var result = await _service.GetByIdAsync(entity.Id);
            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void CreateAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            var dto = new VehicleCreateDTO()
            {
                ChassisSeries = entity.ChassisSeries,
                ChassisNumber = entity.ChassisNumber,
                Type = entity.Type,
                Color = entity.Color,
            };
            _mockRepository.Setup(r => r.InsertAsync(It.IsAny<Vehicle>())).ReturnsAsync(true);
            // Act
            var success = await _service.CreateAsync(dto);
            // Assert
            Assert.True(success);
        }

        [Fact]
        public async void UpdateAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            string newColor = "Yellow";
            var dto = new VehicleUpdateDTO()
            {
                Id = entity.Id,
                Color = newColor,
            };
            entity = _mapper.Map<VehicleUpdateDTO, Vehicle>(dto);
            _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Vehicle>())).ReturnsAsync(true);
            // Act
            var success = await _service.UpdateAsync(dto);
            // Assert
            Assert.True(success);
            Assert.Equal(newColor, entity?.Color);
        }

        [Fact]
        public async void DeleteAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            var recordId = entity.Id;
            _mockRepository.Setup(r => r.GetByIdAsync(recordId)).ReturnsAsync(entity);
            _mockRepository.Setup(r => r.DeleteAsync(entity)).ReturnsAsync(true);
            // Act
            var success = await _service.DeleteAsync(recordId);
            // Assert
            Assert.True(success);
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
            List<Vehicle> entityList = [entity];
            _mockRepository.Setup(r => r.GetByChassisId(cSeries, cNumber)).ReturnsAsync(entityList);
            // Act
            var result = await _service.GetByChassisId(cSeries, cNumber);
            var firstResult = result.FirstOrDefault();
            // Assert
            Assert.NotEmpty(result);
            Assert.Equal(cSeries, firstResult?.ChassisSeries);
            Assert.Equal(cNumber, firstResult?.ChassisNumber);
        }
    }
}