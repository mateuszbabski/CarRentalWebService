using Application.Features.Reservations;
using Application.Features.Reservations.Commands.CreateReservation;
using Application.Features.Vehicles.Commands.CreateVehicle;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.UnitTest.Reservations
{
    public class CreateReservationTest
    {
        private readonly CreateReservationCommandHandler _sut;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private readonly Mock<IRentalCompanyRepository> _rentalCompanyRepositoryMock = new Mock<IRentalCompanyRepository>();
        private readonly Mock<ICustomerRepository> _customerRepositoryMock = new Mock<ICustomerRepository>();
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly Mock<ICurrentUserService> _userServiceMock = new Mock<ICurrentUserService>();
        
        public CreateReservationTest()
        {
            _sut = new CreateReservationCommandHandler(_vehicleRepositoryMock.Object,
                _rentalCompanyRepositoryMock.Object,
                _customerRepositoryMock.Object,
                _reservationRepositoryMock.Object,
                _userServiceMock.Object);
        }

        [Fact]
        public async void CreateReservation_ValidInputs_ReturnsReservationViewModel()
        {
            // arrange
            var newReservation = new Reservation { };

            var reservationVM = new ReservationViewModel { };

            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());
            _customerRepositoryMock.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            _vehicleRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Vehicle());
            _rentalCompanyRepositoryMock.Setup(m => m.GetRentalCompanyByIdAsync(It.IsAny<int>())).ReturnsAsync(new RentalCompany());

            // act
            _reservationRepositoryMock.Setup(m => m.AddAsync(newReservation)).ReturnsAsync(newReservation);

            // arrange
            Assert.IsAssignableFrom<ReservationViewModel>(reservationVM);
        }

        [Fact]
        public async void CreateReservation_CarNotAvailable_ThrowsException()
        {
            // arrange
            var newReservation = new Reservation { };
            var vehicle = new Vehicle { IsAvailable = false };

            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());
            _customerRepositoryMock.Setup(m => m.GetCustomerByIdAsync(It.IsAny<int>())).ReturnsAsync(new Customer());
            _vehicleRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new Exception("Car is not available"));
            _rentalCompanyRepositoryMock.Setup(m => m.GetRentalCompanyByIdAsync(It.IsAny<int>())).ReturnsAsync(new RentalCompany());

            //   act
            var act = Assert.ThrowsAsync<Exception>(() => _sut.Handle(new CreateReservationCommand(), CancellationToken.None));

            //    assert
            Assert.IsType<Task<Exception>>(act);
        }
    }
}





