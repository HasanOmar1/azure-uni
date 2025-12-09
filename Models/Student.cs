using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Models
{
    public class Student
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AvgGrade { get; set; }
        public Address[] Addresses { get; set; }
        public Course[] Courses { get; set; }
    }
}
