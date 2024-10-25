using Microsoft.Extensions.Logging;
using Moq;
using System.Linq.Expressions;
using Vehicles.Api.Repositories;
using Vehicles.Api.Services;

namespace Tests
{
    public class VehiclesServiceTests
    {
        private Mock<IVehiclesRepository> _mockRepository;
        private Mock<ILogger<VehiclesService>> _logger;
        private VehiclesService _vehiclesService;
        private List<Vehicle> _mockVehicles;

        // Common setup for all tests
        public VehiclesServiceTests()
        {
            // Initialize mock vehicle data
            _mockVehicles = new List<Vehicle>
            {
                new Vehicle { Make = "Toyota", Model = "Camry", Trim = "LE", Colour = "Red" },
                new Vehicle { Make = "Honda", Model = "Accord", Trim = "EX", Colour = "Black" },
                new Vehicle { Make = "Ford", Model = "Focus", Trim = "SE", Colour = "Blue" }
            };

            // Initialize the mock repository
            _mockRepository = new Mock<IVehiclesRepository>();
            _logger = new Mock<ILogger<VehiclesService>>();

            // Initialize the service with the mock repository
            _vehiclesService = new VehiclesService(_mockRepository.Object, _logger.Object);
        }

        // Test 1: GetAll should return all vehicles from the repository
        [Fact]
        public void GetAll_ShouldReturnAllVehicles()
        {
            // Arrange
            _mockRepository.Setup(repo => repo.GetAll()).Returns(_mockVehicles);

            // Act
            var result = _vehiclesService.GetAll();

            // Assert
            Assert.Equal(3, result.Count); // Should return 3 vehicles
            Assert.Equal("Toyota", result[0].Make);
            Assert.Equal("Honda", result[1].Make);
            Assert.Equal("Ford", result[2].Make);

            // Verify that GetAll was called once
            _mockRepository.Verify(repo => repo.GetAll(), Times.Once);
        }

        // Test 2: GetByMake should filter vehicles by make
        [Fact]
        public void GetByMake_ShouldReturnVehiclesWithSpecifiedMake()
        {
            // Arrange - Setup the repository to return vehicles filtered by make
            _mockRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Vehicle, bool>>>(), null, It.IsAny<int>(), It.IsAny<int>()))
                           .Returns((Expression<Func<Vehicle, bool>> filter,
                                     Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy,
                                     int take, int page) => _mockVehicles.Where(filter.Compile()));

            // Act
            var result = _vehiclesService.GetByMake("Honda");

            // Assert
            Assert.Single(result); // Only one Honda in the data
            Assert.Equal("Honda", result.First().Make);

            // Verify that Get was called with the correct expression
            _mockRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<Vehicle, bool>>>(), null, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        // Test 3: GetByMake should return an empty list if no vehicles match
        [Fact]
        public void GetByMake_ShouldReturnEmptyListIfNoMatches()
        {
            // Arrange - No vehicle matches the make "Tesla"
            _mockRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Vehicle, bool>>>(), null, It.IsAny<int>(), It.IsAny<int>()))
                           .Returns((Expression<Func<Vehicle, bool>> filter,
                                     Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy,
                                     int take, int page) => _mockVehicles.Where(filter.Compile()));

            // Act
            var result = _vehiclesService.GetByMake("Tesla");

            // Assert
            Assert.Empty(result); // No Tesla in the data

            // Verify that Get was called once
            _mockRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<Vehicle, bool>>>(), null, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        // Test 4: GetByModel should filter vehicles by model
        [Fact]
        public void GetByModel_ShouldReturnVehiclesWithSpecifiedModel()
        {
            // Arrange - Setup repository to return vehicles filtered by model
            _mockRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Vehicle, bool>>>(), null, It.IsAny<int>(), It.IsAny<int>()))
                           .Returns((Expression<Func<Vehicle, bool>> filter,
                                     Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy,
                                     int take, int page) => _mockVehicles.Where(filter.Compile()));

            // Act
            var result = _vehiclesService.GetByModel("Camry");

            // Assert
            Assert.Single(result); // Only one Camry in the data
            Assert.Equal("Camry", result.First().Model);

            // Verify that Get was called once
            _mockRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<Vehicle, bool>>>(), null, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        // Test 5: GetByModel should return an empty list if no vehicles match the model
        [Fact]
        public void GetByModel_ShouldReturnEmptyListIfNoMatches()
        {
            // Arrange - No vehicle matches the model "Civic"
            _mockRepository.Setup(repo => repo.Get(It.IsAny<Expression<Func<Vehicle, bool>>>(), null, It.IsAny<int>(), It.IsAny<int>()))
                           .Returns((Expression<Func<Vehicle, bool>> filter,
                                     Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy,
                                     int take, int page) => _mockVehicles.Where(filter.Compile()));

            // Act
            var result = _vehiclesService.GetByModel("Civic");

            // Assert
            Assert.Empty(result); // No Civic in the data

            // Verify that Get was called once
            _mockRepository.Verify(repo => repo.Get(It.IsAny<Expression<Func<Vehicle, bool>>>(), null, It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }
    }
}