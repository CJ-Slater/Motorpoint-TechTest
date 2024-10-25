using Moq;
using Vehicles.Api.Repositories;

namespace Tests
{
    public class VehicleRepositoryTests
    {
        private IEnumerable<Vehicle> _mockVehicles;
        private VehiclesRepository _repo;

        // Most of the tests use the same set of mock data so set it here - can override if needed.
        public VehicleRepositoryTests()
        {
            // Initialize mock data
            _mockVehicles = new List<Vehicle>
            {
                new Vehicle { Make = "Toyota", Model = "Camry", Trim = "Blue", Colour = "Red" },
                new Vehicle { Make = "Honda", Model = "Accord", Trim = "Blue", Colour = "Black" },
                new Vehicle { Make = "Ford", Model = "Focus", Trim = "Black", Colour = "Blue" }
            };

            // Initialize repository with mock data
            _repo = new VehiclesRepository(_mockVehicles);
        }

        //Ensure the repository correctly loads mock data
        [Fact]
        public void GetAll_ShouldReturnAllVehicles()
        {
            // Act
            _mockVehicles = new List<Vehicle>
            {
                new Vehicle { Make = "Toyota", Model = "Camry", Trim = "LE", Colour = "Red" }
            };
            _repo = new VehiclesRepository(_mockVehicles);
            var result = _repo.GetAll();

            // Assert
            Assert.Single(result);
            Assert.Contains(result, v => v.Make == "Toyota");
        }

        //Test filtering logic
        [Fact]
        public void Get_ShouldFilterVehiclesByMake()
        {
            // Act
            var result = _repo.Get(v => v.Make == "Honda");

            // Assert
            Assert.Single(result);
            Assert.Equal("Honda", result.First().Make);
        }

        // Test pagination
        [Fact]
        public void Get_ShouldPaginateResults()
        {
            // Act - Get second page with 1 item per page
            var result = _repo.Get(take: 1, page: 1);

            // Assert
            Assert.Single(result);
            Assert.Equal("Honda", result.First().Make);
        }

        // Test ordering
        [Fact]
        public void Get_ShouldOrderResultsByModel()
        {
            // Act - Order by Model in ascending order
            var result = _repo.Get(orderBy: q => q.OrderBy(v => v.Model));

            // Assert
            Assert.Equal("Accord", result.First().Model); // Honda Accord comes first in alphabetical order
            Assert.Equal("Focus", result.Last().Model);   // Ford Focus comes last
        }

        // Test with an empty list
        [Fact]
        public void GetAll_ShouldReturnEmptyListIfNoVehicles()
        {
            // Arrange - Empty repository
            _repo = new VehiclesRepository(new List<Vehicle>());

            // Act
            var result = _repo.GetAll();

            // Assert
            Assert.Empty(result);
        }

        // Testing filter and pagination together
        [Fact]
        public void Get_ShouldApplyFilterAndPaginationTogether()
        {
            // Act - Filter by trim 'Blue' and paginate with 1 result per page, second page
            var result = _repo.Get(v => v.Trim == "Blue", take: 1, page: 1);

            // Assert
            Assert.Single(result);
            Assert.Equal("Accord", result.First().Model); // Honda Accord should be on the second page
        }
    }
}