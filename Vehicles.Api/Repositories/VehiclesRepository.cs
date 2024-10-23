using System.Text.Json;
using System;
using System.Text.Json.Serialization;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Vehicles.Api.Repositories
{

    public class Vehicle
    {
        [JsonPropertyName("make")]
        public string Make { get; set; }
        [JsonPropertyName("model")]
        public string Model { get; set; }
        [JsonPropertyName("trim")]
        public string Trim { get; set; }
        [JsonPropertyName("colour")]
        public string Colour { get; set; }
    }

    public class VehiclesRepository
    {
        List<Vehicle> _vehicles;
        public VehiclesRepository()
        {
            using (StreamReader r = new StreamReader("Repositories/vehicles.json"))
            {
                string json = r.ReadToEnd();
                _vehicles = JsonSerializer.Deserialize<List<Vehicle>>(json) ?? new List<Vehicle>();
            }
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

            if (take >= 0)
                query = query.Take(take);

            if (page >= 0)
            query = query.Skip(take * page);


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
