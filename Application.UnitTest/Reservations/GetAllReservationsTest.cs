using Application.Exceptions;
using Application.Features.Reservations;
using Application.Features.Reservations.Queries.GetAllReservationsList;
using Application.Features.Vehicles;
using Application.Features.Vehicles.Queries.GetVehicleById;
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
    public class GetAllReservationsTest
    {
        private readonly GetAllReservationsListQueryHandler _sut;
        private readonly Mock<ICurrentUserService> _userServiceMock = new Mock<ICurrentUserService>();
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        public GetAllReservationsTest()
        {
            _sut = new GetAllReservationsListQueryHandler(
                _userServiceMock.Object, 
                _mapperMock.Object, 
                _reservationRepositoryMock.Object);
        }

        [Fact]
        public async void GetAllReservations_ValidData_ReturnsReservationsList()
        {
            // arrange
            var expectedReservationList = new List<ReservationViewModel> 
            {
                new ReservationViewModel
                {
                    Brand = "BMW"
                },
                new ReservationViewModel
                {
                    Brand = "Audi"
                }
            };

            IEnumerable<Reservation> reservations = new List<Reservation>();
            IEnumerable<ReservationViewModel> reservationsVM = expectedReservationList;

            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());

            _reservationRepositoryMock
                .Setup(m => m.GetAllReservationsForCustomerIdAsync(It.IsAny<int>()))
                .ReturnsAsync(reservations);

            _mapperMock.Setup(m => m.Map<IEnumerable<ReservationViewModel>>(It.IsAny<IEnumerable<Reservation>>())).Returns(expectedReservationList);

            // act
            var reservationList = await _sut.Handle(new GetAllReservationsListQuery(), CancellationToken.None);

            // assert

            Assert.Equal(2, reservationList.Count());
            Assert.IsAssignableFrom<IEnumerable<ReservationViewModel>>(reservationList);
        }

        [Fact]
        public async void GetAllReservations_DataNotFound_ThrowsNotFoundException()
        {
            // arrange
            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());

            _reservationRepositoryMock
                .Setup(m => m.GetAllReservationsForCustomerIdAsync(It.IsAny<int>()))
                .ThrowsAsync(new NotFoundException());

            //act
            var act = Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new GetAllReservationsListQuery(), CancellationToken.None));

            //assert
            Assert.IsType<Task<NotFoundException>>(act);

        }
    }
}

            
            
