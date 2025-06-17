using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolvoWebApp.Configurations;
using VolvoWebApp.Controllers;
using VolvoWebApp.Data.Entities;
using VolvoWebApp.Dtos;
using VolvoWebApp.Enums;
using VolvoWebApp.Models;
using VolvoWebApp.Services;

namespace VolvoWebAppTests.Controllers
{
    public class VehiclesControllerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IVehiclesService> _mockService;
        private readonly VehiclesController _controller;

        private readonly List<Vehicle> _testEntities =
        [
            new() { ChassisSeries = "ChassisSeriesA", ChassisNumber = 1, Type = VehicleType.Bus, Color = "Red" },
            new() { ChassisSeries = "ChassisSeriesB", ChassisNumber = 1, Type = VehicleType.Car, Color = "Blue" },
            new() { ChassisSeries = "ChassisSeriesC", ChassisNumber = 2, Type = VehicleType.Truck, Color = "Green" },
        ];

        public VehiclesControllerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new VolvoProfile());
            });
            _mapper = config.CreateMapper();
            _mockService = new Mock<IVehiclesService>();
            _controller = new VehiclesController(_mapper, _mockService.Object);
        }

        [Fact]
        public async Task GetAllAsync()
        {
            // Arrange
            var entities = _testEntities;
            var dtos = _mapper.Map<IEnumerable<VehicleReadDTO>>(entities);
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(dtos);
            // Act
            var result = await _controller.Index();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<VehicleModel>>(viewResult.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async void GetByIdAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            var dto = _mapper.Map<VehicleReadDTO>(entity);
            _mockService.Setup(r => r.GetByIdAsync(entity.Id)).ReturnsAsync(dto);
            // Act
            var result = await _controller.Details(entity.Id);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<VehicleModel>(viewResult.Model);
            Assert.NotNull(model);
        }

        [Fact]
        public async void CreateAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            var readDto = _mapper.Map<VehicleReadDTO>(entity);
            var model = _mapper.Map<VehicleModel>(readDto);
            //var arrangeDto = new VehicleCreateDTO()
            //{
            //    ChassisSeries = entity.ChassisSeries,
            //    ChassisNumber = entity.ChassisNumber,
            //    Type = entity.Type,
            //    Color = entity.Color,
            //};
            _mockService.Setup(r => r.CreateAsync(It.IsAny<VehicleCreateDTO>())).ReturnsAsync(true);
            // Act
            var result = await _controller.Create(model);
            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void UpdateAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            var readDto = _mapper.Map<VehicleReadDTO>(entity);
            var model = _mapper.Map<VehicleModel>(readDto);
            //var arrangeDto = new VehicleUpdateDTO()
            //{
            //    Id = entity.Id,
            //    Color = entity.Color,
            //};
            _mockService.Setup(r => r.UpdateAsync(It.IsAny<VehicleUpdateDTO>())).ReturnsAsync(true);
            // Act
            var result = await _controller.Edit(model.Id, model);
            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public async void DeleteAsync()
        {
            // Arrange
            var entity = _testEntities[0];
            var readDto = _mapper.Map<VehicleReadDTO>(entity);
            var model = _mapper.Map<VehicleModel>(readDto);
            _mockService.Setup(r => r.DeleteAsync(model.Id)).ReturnsAsync(true);
            // Act
            var result = await _controller.DeleteConfirmed(model.Id);
            // Assert
            var viewResult = Assert.IsType<RedirectToActionResult>(result);
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
            var dto = _mapper.Map<VehicleReadDTO>(entity);
            IEnumerable<VehicleReadDTO> dtoList = [dto];
            _mockService.Setup(r => r.GetByChassisId(cSeries, cNumber)).ReturnsAsync(dtoList);
            // Act
            var result = await _controller.ShowSearchByChassisResults(cSeries, cNumber);
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<VehicleModel>>(viewResult.Model);
            Assert.NotEmpty(model);
        }
    }
}
