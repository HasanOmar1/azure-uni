using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Models
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNum { get; set; }


        public override string ToString()
        {
            string addressDataAsStr = string.Empty;

            addressDataAsStr += (string.IsNullOrEmpty(City)) ? "\nNo City is documented at this moment" : "\nCity: " + City;

            addressDataAsStr += (string.IsNullOrEmpty(Street)) ? "\nNo Street is documented at this moment" : "\nStreet: " + Street;

            addressDataAsStr += (string.IsNullOrEmpty(HouseNum)) ? "\nNo HouseNum is documented at this moment" : "\nHouseNum: " + HouseNum;

            return addressDataAsStr;
        }
    }


}
