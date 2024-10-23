using Vehicles.Api.Repositories;

namespace Vehicles.Api.Services
{
    public class VehiclesService
    {
        private readonly VehiclesRepository _vehiclesRepository;
        public VehiclesService(VehiclesRepository vehiclesRepository) { 
            _vehiclesRepository = vehiclesRepository;
        }

        public List<Vehicle> GetAll()
        {
            return _vehiclesRepository.GetAll();
        }

        public IEnumerable<Vehicle> GetByMake(string make)
        {
            return _vehiclesRepository.Get(c => c.Make == make);
        }

        public IEnumerable<Vehicle> GetByModel(string model)
        {
            return _vehiclesRepository.Get(c => c.Model == model);
        }
    }
}
