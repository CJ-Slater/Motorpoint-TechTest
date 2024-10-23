using Vehicles.Api.Repositories;

namespace Vehicles.Api.Services
{
    public interface IVehiclesService
    {
        List<Vehicle> GetAll();
        IEnumerable<Vehicle> GetByMake(string make);
        IEnumerable<Vehicle> GetByModel(string model);
        IEnumerable<Vehicle> GetUnderPrice(decimal price);
        IEnumerable<Vehicle> GetInPriceRange(decimal lowerPrice, decimal upperPrice);
    }
}