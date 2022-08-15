using Application.Exceptions;
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

namespace Application.UnitTest.Vehicles
{
    public class GetVehicleByIdTest
    {
        private readonly GetVehicleByIdQueryHandler _sut;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public GetVehicleByIdTest()
        {
            _sut = new GetVehicleByIdQueryHandler(_vehicleRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void GetVehicleById_WithExistId_ReturnsVehicle()
        {
            //arrange
            var vehicle = new VehicleViewModel
            {
                Id = 1
            };

            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(new Domain.Entities.Vehicle());
            _mapperMock.Setup(m => m.Map<VehicleViewModel>(It.IsAny<Domain.Entities.Vehicle>())).Returns(vehicle);

            //act
            var result = await _sut.Handle(new GetVehicleByIdQuery(), CancellationToken.None);

            //assert
            Assert.IsType<VehicleViewModel>(result);
            Assert.Equal(1, vehicle.Id);
            
        }
        [Fact]
        public async void GetVehicleById_WithNonExistId_ThrowsNotFound()
        {
            //arrange
            _vehicleRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>())).Throws<NotFoundException>();

            //act
            var act = Assert.ThrowsAsync<NotFoundException>(() => _sut.Handle(new GetVehicleByIdQuery(), CancellationToken.None));
            
            //assert
            Assert.IsType<Task<NotFoundException>>(act);
        }
    }
}
            



