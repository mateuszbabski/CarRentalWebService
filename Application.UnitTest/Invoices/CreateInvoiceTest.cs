using Application.Exceptions;
using Application.Features.Invoices.Commands.CreateInvoice;
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

namespace Application.UnitTest.Invoices
{
    public class CreateInvoiceTest
    {
        private readonly CreateInvoiceCommandHandler _sut;
        private readonly Mock<ICurrentUserService> _userServiceMock = new Mock<ICurrentUserService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
        private readonly Mock<IReservationRepository> _reservationRepositoryMock = new Mock<IReservationRepository>();
        public CreateInvoiceTest()
        {
            _sut = new CreateInvoiceCommandHandler(_invoiceRepositoryMock.Object, 
                _mapperMock.Object, 
                _userServiceMock.Object, 
                _reservationRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateInvoice_ValidInputs_ReturnsInvoiceId()
        {
            // arrange
            var newInvoice = new Invoice { Id = 1};
            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());
            _reservationRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Reservation>())).ReturnsAsync(new Reservation());
            _invoiceRepositoryMock.Setup(m => m.AddAsync(newInvoice)).ReturnsAsync(newInvoice);

            // act
            var result = _sut.Handle(new CreateInvoiceCommand(), CancellationToken.None);

            // assert
            Assert.Equal(1, newInvoice.Id);
            Assert.IsType<Task<int>>(result);

        }

        [Fact]
        public async Task CreateInvoice_InvalidReservationId_ThrowsException()
        {
            // arrange
            var newInvoice = new Invoice { Id = 1 };
            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());
            _reservationRepositoryMock.Setup(m => m.AddAsync(It.IsAny<Reservation>())).ThrowsAsync(new NotFoundException());

            // act
            var act = Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new CreateInvoiceCommand(), CancellationToken.None));

            // assert
            Assert.IsType<Task<NotFoundException>>(act);
        }
    }
}
