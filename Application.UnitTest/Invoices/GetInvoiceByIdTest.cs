using Application.Exceptions;
using Application.Features.Invoice;
using Application.Features.Invoices.Queries.GetInvoiceById;
using Application.Features.Reservations;
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

namespace Application.UnitTest.Invoices
{
    public class GetInvoiceByIdTest
    {
        private readonly GetInvoiceByIdQueryHandler _sut;
        private readonly Mock<ICurrentUserService> _userServiceMock = new Mock<ICurrentUserService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
        public GetInvoiceByIdTest()
        {
            _sut = new GetInvoiceByIdQueryHandler(_mapperMock.Object, _userServiceMock.Object, _invoiceRepositoryMock.Object);
        }

        [Fact]
        public async Task GetInvoiceById_IdExists_ReturnsInvoice()
        {
            // arrange
            var invoiceEntity = new InvoiceViewModel();
            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());
            _invoiceRepositoryMock
                .Setup(m => m.GetInvoiceByIdForCustomerAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Invoice());

            _mapperMock.Setup(m => m.Map<InvoiceViewModel>(It.IsAny<Invoice>())).Returns(invoiceEntity);

            // act
            var invoiceList = await _sut.Handle(new GetInvoiceByIdQuery(), CancellationToken.None);

            // assert
            Assert.IsType<InvoiceViewModel>(invoiceList);

        }

        [Fact]
        public async Task GetInvoiceById_IdDoesntExist_ThrowsNotFoundException()
        {
            // arrange
            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());

            _invoiceRepositoryMock
                .Setup(m => m.GetInvoiceByIdForCustomerAsync(It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new NotFoundException());

            //act
            var act = Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new GetInvoiceByIdQuery(), CancellationToken.None));

            //assert
            Assert.IsType<Task<NotFoundException>>(act);
        }
    }
}
