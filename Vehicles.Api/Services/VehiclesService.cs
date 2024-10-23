using Vehicles.Api.Repositories;

namespace Vehicles.Api.Services
{
    public class VehiclesService
    {
        private readonly IVehiclesRepository _vehiclesRepository;
        private readonly ILogger<VehiclesService> _logger;
        public VehiclesService(IVehiclesRepository vehiclesRepository, ILogger<VehiclesService> logger)
        {
            _vehiclesRepository = vehiclesRepository;
            _logger = logger;
        }

        public List<Vehicle> GetAll()
        {
            //Not putting a try/catch here as would rather notify front-end if there was an issue here and let that handle it.
            return _vehiclesRepository.GetAll() ?? new List<Vehicle>();
        }

        public IEnumerable<Vehicle> GetByMake(string make)
        {
            if (string.IsNullOrWhiteSpace(make))
            {
                _logger.LogWarning("GetByMake was called with a null or empty make.");
                return Enumerable.Empty<Vehicle>();
            }

            try
            {
                return _vehiclesRepository.Get(c => c.Make == make) ?? Enumerable.Empty<Vehicle>();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Bad query structure while retrieving vehicles for make: {make}");
                throw; // Rethrow to propagate the error - would rather it bubble up so user/front-end is aware and can handle it (if it's not something that can be handled here).
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving vehicles by make.");
                throw;
            }
        }

        public IEnumerable<Vehicle> GetByModel(string model)
        {
            if (string.IsNullOrWhiteSpace(model))
            {
                _logger.LogWarning("GetByMake was called with a null or empty make.");
                return Enumerable.Empty<Vehicle>();
            }

            try
            {
                return _vehiclesRepository.Get(c => c.Model == model) ?? Enumerable.Empty<Vehicle>();
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, $"Bad query structure while retrieving vehicles for model: {model}");
                throw;
            }
            catch (Exception ex) // Catching all other unexpected exceptions
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving vehicles by model.");
                throw;
            }
        }
    }
}
