using Application.Exceptions;
using Application.Features.Reservations;
using Application.Features.Reservations.Queries.GetAllReservationsList;
using Application.Features.Reservations.Queries.GetReservationById;
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
    public class GetReservationByIdTest
    {
        private readonly GetReservationByIdQueryHandler _sut;
        private readonly Mock<ICurrentUserService> _userServiceMock = new Mock<ICurrentUserService>();
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        public GetReservationByIdTest()
        {
            _sut = new GetReservationByIdQueryHandler(
                _userServiceMock.Object, 
                _mapperMock.Object, 
                _reservationRepositoryMock.Object);
        }

        [Fact]
        public async void GetReservationById_IdExists_ReturnsReservationViewModel()
        {
            // arrange
            var reservationEntity = new ReservationViewModel();
            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());

            _reservationRepositoryMock
                .Setup(m => m.GetReservationByIdForCustomerAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Reservation());

            _mapperMock.Setup(m => m.Map<ReservationViewModel>(It.IsAny<Reservation>())).Returns(reservationEntity);

            // act
            var reservationList = await _sut.Handle(new GetReservationByIdQuery(), CancellationToken.None);

            // assert
            Assert.IsType<ReservationViewModel>(reservationList);
        }

        [Fact]
        public async void GetReservationById_IdDoesntExist_ThrowsNotFoundException()
        {
            // arrange
            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());

            _reservationRepositoryMock
                .Setup(m => m.GetReservationByIdForCustomerAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new NotFoundException());

            //act
            var act = Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new GetReservationByIdQuery(), CancellationToken.None));

            //assert
            Assert.IsType<Task<NotFoundException>>(act);
        }
    }
}
            

