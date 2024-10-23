using Microsoft.AspNetCore.Mvc;
using Vehicles.Api.Repositories;
using Vehicles.Api.Services;

namespace Vehicles.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehiclesController : ControllerBase
    {
        private readonly ILogger<VehiclesController> _logger;
        private readonly IVehiclesService _vehiclesService;

        public VehiclesController(ILogger<VehiclesController> logger, IVehiclesService vehiclesService)
        {
            _logger = logger;
            _vehiclesService = vehiclesService;
        }


        [HttpGet]
        public IActionResult GetVehicles()
        {
            try
            {
                var vehicles = _vehiclesService.GetAll();
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all vehicles.");
                return StatusCode(500, "An error occurred while processing your request."); //Return friendly message to user in case of any errors that haven't been handled by this point.
            }
        }

        [HttpGet]
        [Route("get-by-make")]
        public IActionResult GetVehiclesByMake(string make)
        {
            if (String.IsNullOrEmpty(make))
                return BadRequest("No make was specified.");

            try
            {
                var vehicles = _vehiclesService.GetByMake(make);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving vehicles for make: {make}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("get-by-model")]
        public IActionResult GetVehiclesByModel(string model)
        {
            if (String.IsNullOrEmpty(model))
                return BadRequest("No model was specified.");

            try
            {
                var vehicles = _vehiclesService.GetByModel(model);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving vehicles for model: {model}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("get-under-price")]
        public IActionResult GetVehiclesUnderPrice(decimal price)
        {
            if (price <= 0)
                return BadRequest("Price given must be above 0.");

            try
            {
                var vehicles = _vehiclesService.GetUnderPrice(price);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving vehicles under price model: {price}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [Route("get-in-price-range")]
        public IActionResult GetVehiclesInPriceRange(decimal lowerPrice, decimal upperPrice)
        {
            if (lowerPrice < 0 || upperPrice <= 0)
                return BadRequest("Price range must be above 0.");

            if (lowerPrice > upperPrice)
                return BadRequest("Lower price must be less than upper price.");

            try
            {
                var vehicles = _vehiclesService.GetInPriceRange(lowerPrice, upperPrice);
                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving vehicles in price range: {lowerPrice} - {upperPrice}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}