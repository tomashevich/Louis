using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Louis.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
