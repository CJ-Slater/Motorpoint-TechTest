using System.Text.Json;
using System;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Vehicles.Api.Repositories
{

    public class Vehicle
    {
        [JsonProperty("make")]
        public string Make { get; set; }
        [JsonProperty("model")]
        public string Model { get; set; }
        [JsonProperty("trim")]
        public string Trim { get; set; }
        [JsonProperty("colour")]
        public string Colour { get; set; }
        [JsonProperty("price")]
        public decimal Price { get; set; }
        [JsonProperty("engine_size")]
        public int EngineSize { get; set; }
        [JsonProperty("co2_level")]
        public int CO2Level { get; set; }
        [JsonProperty("transmission")]
        public string Transmission { get; set; }
        [JsonProperty("fuel_type")]
        public string FuelType { get; set; }
        [JsonProperty("date_first_reg")]
        [JsonConverter(typeof(DateJsonSerializer))]
        public DateTime DateFirstRegistered { get; set; }
        [JsonProperty("mileage")]
        public int Mileage { get; set; }

    }

    //I tried not to modify the given repository class as much as possible since that wasn't the basis of the technical test, but needed to add this interface so I could mock the repo for unit testing.
    public interface IVehiclesRepository
    {
        List<Vehicle> GetAll();
        IEnumerable<Vehicle> Get(Expression<Func<Vehicle, bool>> filter = null, Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy = null, int take = 50, int page = 0);
    }

    public class VehiclesRepository : IVehiclesRepository
    {
        List<Vehicle> _vehicles;
        public VehiclesRepository()
        {
            using (StreamReader r = new StreamReader("Repositories/vehicles.json"))
            {
                string json = r.ReadToEnd();
                _vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(json) ?? new List<Vehicle>();
            }
        }

        
        //Same here, had to add this constructor so I could mock data for testing.
        public VehiclesRepository(IEnumerable<Vehicle> vehicles)
        {
            _vehicles = vehicles?.ToList() ?? new List<Vehicle>();
        }

        public List<Vehicle> GetAll()
        {
            return _vehicles;
        }

        public IEnumerable<Vehicle> Get(Expression<Func<Vehicle, bool>> filter = null, Func<IQueryable<Vehicle>, IOrderedQueryable<Vehicle>> orderBy = null, int take = 50, int page = 0)
        {

            IQueryable<Vehicle> query = _vehicles.AsQueryable();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (page >= 0)
            query = query.Skip(take * page);

            if (take >= 0)
                query = query.Take(take);


            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }
    }
}
