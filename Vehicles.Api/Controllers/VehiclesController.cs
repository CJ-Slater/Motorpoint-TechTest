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
        private readonly VehiclesService _vehiclesService;

        public VehiclesController(ILogger<VehiclesController> logger, VehiclesService vehiclesService)
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
    }
}