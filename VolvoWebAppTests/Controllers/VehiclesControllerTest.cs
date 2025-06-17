using AutoMapper;
using Humanizer;
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
        public async Task Index_ShouldReturnViewWithVehicles()
        {
            // Arrange
            var entities = _testEntities;
            var dtos = _mapper.Map<IEnumerable<VehicleReadDTO>>(entities);
            var models = _mapper.Map<IEnumerable<VehicleReadDTO>>(dtos);
            _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(models);
            // Act
            var result = await _controller.Index();
            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<VehicleModel>>(viewResult.Model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public async Task Details_ShouldReturnNotFound_WhenIdIsNull()
        {
            var result = await _controller.Details(null);
            Assert.IsType<NotFoundResult>(result);
        }

        //[Fact]
        //public async Task Details_ShouldReturnView_WhenVehicleExists()
        //{
        //    var vehicle = new Vehicle { Id = 1, Model = "Truck", Year = 2019 };
        //    _contextMock.Setup(c => c.Vehicles.FindAsync(1)).ReturnsAsync(vehicle);

        //    var result = await _controller.Details(1);

        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsType<Vehicle>(viewResult.Model);
        //    Assert.Equal(vehicle.Id, model.Id);
        //}

        //[Fact]
        //public async Task Create_ShouldRedirectToIndex_WhenModelIsValid()
        //{
        //    var vehicle = new Vehicle { Id = 1, Model = "Bike", Year = 2022 };

        //    var result = await _controller.Create(vehicle);

        //    var redirectResult = Assert.IsType<RedirectToActionResult>(result);
        //    Assert.Equal("Index", redirectResult.ActionName);
        //}

        //[Fact]
        //public async Task Create_ShouldReturnView_WhenModelStateIsInvalid()
        //{
        //    _controller.ModelState.AddModelError("Model", "Required");

        //    var vehicle = new Vehicle { Id = 2, Year = 2023 }; // Model está faltando

        //    var result = await _controller.Create(vehicle);

        //    Assert.IsType<ViewResult>(result);
        //}
    }
}
