using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData
{
    internal class Product
    {
        public required int Id { get; set; }

        public string Name { get; set; }
        public string Category { get; set; }

        //public int Relea { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }
        //public string Location { get; set; }
    }
}
