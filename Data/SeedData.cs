using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace AdvApi.Data
{
    public class SeedData
    {
        private protected readonly ApiContext _db;

        public SeedData(ApiContext db)
        {
            _db = db;

            if (SeedCustomers())
            {
                Console.WriteLine("Customer data seeded.");
            }
            else
            {
                Console.WriteLine("ERROR: Unable to seed Customer data.");
            }

            if (SeedOrders())
            {
                Console.WriteLine("Order data seeded.");
            }
            else
            {
                Console.WriteLine("ERROR: Unable to seed Order data.");
            }

            if (SeedServers())
            {
                Console.WriteLine("Server data seeded.");
            }
            else
            {
                Console.WriteLine("ERROR: Unable to seed Server data.");
            }
        }

        public bool SeedCustomers()
        {
            string filename = "../Json/Customers.json";
            var file = File.ReadAllText(filename);
            if (file == null) { return false; }
            string customers = JsonSerializer.Deserialize<List<Customer>>(file).ToString();
            _db.AddRange(customers);
            _db.SaveChanges();
            return true;
        }

        public bool SeedOrders()
        {
            string filename = "../Json/Orders.Json";
            var file = File.ReadAllText(filename);
            if (file == null) { return false; }
            string orders = JsonSerializer.Deserialize<List<Order>>(file).ToString();
            _db.AddRange(orders);
            _db.SaveChanges();
            return true;
        }

        public bool SeedServers()
        {
            string filename = "../Json/Servers.Json";
            var file = File.ReadAllText(filename);
            if (file == null) { return false; }
            string servers = JsonSerializer.Deserialize<List<Server>>(file).ToString();
            _db.AddRange(servers);
            _db.SaveChanges();
            return true;
        }

    }
}
