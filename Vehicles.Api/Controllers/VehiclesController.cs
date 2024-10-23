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
        public List<Vehicle> GetVehicles()
        {
            return _vehiclesService.GetAll();
        }

        [HttpGet]
        [Route("get-by-make")]
        public IActionResult GetVehiclesByMake(string make)
        {
            if (String.IsNullOrEmpty(make))
                return BadRequest("No make was specified.");

            return Ok(_vehiclesService.GetByMake(make));
        }

        [HttpGet]
        [Route("get-by-model")]
        public IActionResult GetVehiclesByModel(string model)
        {
            if (String.IsNullOrEmpty(model))
                return BadRequest("No model was specified.");

            return Ok(_vehiclesService.GetByModel(model));
        }
    }
}