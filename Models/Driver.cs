using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Models
{
    public class Driver
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int YearsInService { get; set; }
        public double Age { get; set; }
        public Passenger[] Passengers { get; set; }
        public CabStation[] CabStations { get; set; }


    }
}
