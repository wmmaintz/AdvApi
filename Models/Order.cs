using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdvApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public decimal Total { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Completed { get; set; }

    }
}
