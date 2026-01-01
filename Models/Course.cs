using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloud.Models
{
    public class Course
    {
        public string CourseName { get; set; }
        public string Teacher { get; set; }
        public int Year { get; set; }
        public int SemesterNum { get; set; }
        public int Grade { get; set; }




        public override string ToString()
        {
            string courseDataAsStr = string.Empty;

            courseDataAsStr += (string.IsNullOrEmpty(CourseName)) ? "\nNo Course Name is documented at this moment" : "\nCourse Name: " + CourseName;

            courseDataAsStr += (string.IsNullOrEmpty(Teacher)) ? "\nNo Teacher is documented at this moment" : "\nTeacher: " + Teacher;


            return courseDataAsStr;
        }
    }
}
