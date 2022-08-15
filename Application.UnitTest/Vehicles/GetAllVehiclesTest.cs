using Application.Features.Vehicles;
using Application.Features.Vehicles.Queries.GetAllVehiclesList;
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
    public class GetAllVehiclesTest
    {
        private readonly GetAllVehiclesListQueryHandler _sut;
        private readonly Mock<IVehicleRepository> _vehicleRepositoryMock = new Mock<IVehicleRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        public GetAllVehiclesTest()
        {
            _sut = new GetAllVehiclesListQueryHandler(_vehicleRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async void GetAllVehicles_ExistData_ReturnsVehiclesList()
        {
            //arrange
            var expectedVehiclesList = new List<VehicleViewModel>
            {
                new VehicleViewModel
                {
                    Id = 1,
                    Brand = "BMW"
                },
                new VehicleViewModel
                {
                    Id = 2,
                    Brand = "Audi"
                }
            };

            IEnumerable<Vehicle> vehicles = new List<Vehicle>();
            IEnumerable<VehicleViewModel> expectedEnumerableVehicleList = expectedVehiclesList;

            _vehicleRepositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(vehicles);

            _mapperMock
                .Setup(m => m.Map<IEnumerable<VehicleViewModel>>(It.IsAny<IEnumerable<Vehicle>>()))
                .Returns(expectedEnumerableVehicleList);

            //act
            var vehicleList = await _sut.Handle(new GetAllVehiclesListQuery(), CancellationToken.None);

            //assert
            Assert.IsAssignableFrom<IEnumerable<VehicleViewModel>>(vehicleList);
            Assert.Equal(2, vehicleList.Count());

        }
    }
}
