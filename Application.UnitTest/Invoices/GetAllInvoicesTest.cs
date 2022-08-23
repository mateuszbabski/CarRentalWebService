using Application.Features.Invoice;
using Application.Features.Invoices.Queries.GetAllInvoicesList;
using Application.Features.Reservations;
using Application.Features.Reservations.Queries.GetAllReservationsList;
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
    public class GetAllInvoicesTest
    {
        private readonly GetAllInvoicesQueryHandler _sut;
        private readonly Mock<ICurrentUserService> _userServiceMock = new Mock<ICurrentUserService>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        private readonly Mock<IInvoiceRepository> _invoiceRepositoryMock = new Mock<IInvoiceRepository>();
        public GetAllInvoicesTest()
        {
            _sut = new GetAllInvoicesQueryHandler(_mapperMock.Object, _userServiceMock.Object, _invoiceRepositoryMock.Object);
        }

        [Fact]
        public async Task GetAllInvoices_DataExist_ReturnsInvoicesList()
        {
            var expectedInvoiceList = new List<InvoiceViewModel>
            {
                new InvoiceViewModel
                {
                    Brand = "BMW"
                },
                new InvoiceViewModel
                {
                    Brand = "Audi"
                }
            };

            IEnumerable<Invoice> invoices = new List<Invoice>();
            IEnumerable<InvoiceViewModel> invoiceVM = expectedInvoiceList;

            _userServiceMock.Setup(m => m.UserId).Returns(It.IsAny<int>());

            _invoiceRepositoryMock
                .Setup(m => m.GetAllInvoicesForCustomerAsync(It.IsAny<int>()))
                .ReturnsAsync(invoices);

            _mapperMock.Setup(m => m.Map<IEnumerable<InvoiceViewModel>>(It.IsAny<IEnumerable<Invoice>>())).Returns(expectedInvoiceList);

            // act
            var invoiceList = await _sut.Handle(new GetAllInvoicesQuery(), CancellationToken.None);

            // assert

            Assert.Equal(2, invoiceList.Count());
            Assert.IsAssignableFrom<IEnumerable<InvoiceViewModel>>(invoiceList);
        }
    }
}
