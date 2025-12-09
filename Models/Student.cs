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

        public static List<Student> ConvertStringIntoList(string studentsAsList)
        {
            if (string.IsNullOrEmpty(studentsAsList)) return new List<Student>();
            return System.Text.Json.JsonSerializer.Deserialize<List<Student>>(studentsAsList);
        }
    }
}
