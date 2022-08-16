using Application.Exceptions;
using Application.Features.Vehicles.Commands.CreateVehicle;
using Application.Features.Vehicles.Queries.GetVehicleById;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ValidationException = Application.Exceptions.ValidationException;

namespace Application.UnitTest.Vehicles
{
    public class CreateVehicleTest
    {
        private readonly CreateVehicleCommandHandler _sut;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<ICurrentUserService> _userServiceMock = new Mock<ICurrentUserService>();
        private readonly Mock<CreateVehicleCommandValidator> _validatorMock = new Mock<CreateVehicleCommandValidator>();
        public CreateVehicleTest()
        {
            _sut = new CreateVehicleCommandHandler(_vehicleRepositoryMock.Object, _mapperMock.Object, _userServiceMock.Object);
        }
        [Fact]
        public async void CreateNewVehicle_ValidFields_ReturnsInt()
        {
            //arrange
            var vehicle = new Vehicle
            {
                Id = 1,
                //Brand = "BMW",
                //Model = "Z3",
                //Type = "Cabrio",
                //NumberOfSeats = 2,
                //FuelType = "Petrol",
                //ProductionYear = 1999,
                //Color = "Black",
                //IsAvailable = true,
                //DailyCost = 300,
                //RentalCompanyId = 1
            };

            _validatorMock
                .Setup(m => m.ValidateAsync(It.IsAny<ValidationContext<CreateVehicleCommand>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());

            _mapperMock.Setup(m => m.Map<Vehicle>(It.IsAny<CreateVehicleCommand>())).Returns(vehicle);

            _vehicleRepositoryMock.Setup(m => m.AddAsync(vehicle)).ReturnsAsync(vehicle);

            //act
            var result = _sut.Handle(new CreateVehicleCommand(), CancellationToken.None);
            //assert
            Assert.Equal(1, vehicle.Id);
            Assert.IsType<Task<int>>(result);
            
        }

        [Fact]
        public async void CreateNewVehicle_InvalidFields_ThrowsValidationException()
        {
            //   arrange
            var validationResult = _validatorMock
                   .Setup(m => m.ValidateAsync(It.IsAny<ValidationContext<CreateVehicleCommand>>(), It.IsAny<CancellationToken>()))
                   .ThrowsAsync(new ValidationException());
            //   act
            var act = Assert.ThrowsAsync<ValidationException>(() => _sut.Handle(new CreateVehicleCommand(), CancellationToken.None));

            //    assert
            Assert.IsType<Task<ValidationException>>(act);
        }
    }
}
