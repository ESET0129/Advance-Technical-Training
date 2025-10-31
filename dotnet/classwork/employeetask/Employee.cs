using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace employeetask
{
    internal class Employee
    {
        public required int Id { get; set; }

        public string Name { get; set; }
        public string Department { get; set; }

        //public int Relea { get; set; }

        public double Salary { get; set; }

        public int Experience { get; set; }
        public string Location { get; set; }
    }
}
