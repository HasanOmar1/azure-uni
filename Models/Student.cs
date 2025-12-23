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
        public string ObjType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double AvgGrade { get; set; }
        public Address[] Addresses { get; set; }
        public Course[] Courses { get; set; }


        public Student()
        {
            ObjType = this.GetType().Name;
        }

        public static List<Student> ConvertStringIntoList(string studentsAsList)
        {
            if (string.IsNullOrEmpty(studentsAsList)) return new List<Student>();
            return System.Text.Json.JsonSerializer.Deserialize<List<Student>>(studentsAsList);
        }

        public override string ToString()
        {
            string studentDataAsStr = string.Empty;
            studentDataAsStr += "Hi, I'm Student #" + id;
            studentDataAsStr += (string.IsNullOrEmpty(FirstName)) ? "\nNo First Name is documented at this moment" : "\nMy First Name is " + FirstName;

            studentDataAsStr += (string.IsNullOrEmpty(LastName)) ? "\nNo Last Name is documented at this moment" : "\nMy Last Name is " + LastName;

            int addrCounter = 0;
            string addrInfo = string.Empty;

            if (Addresses != null && Addresses.Length != 0)
            {
                for (int i = 0; i < Addresses.Length; i++)
                {
                    if (Addresses[i] != null)
                    {
                        addrCounter++;
                        addrInfo += Addresses[i].ToString();
                    }
                }
            }

            studentDataAsStr += (addrCounter == 0) ? "\nNo Addresses are documented at the moment" :
                              $"\n{addrCounter} addresses are documented:{addrInfo}";

            int courseCounter = 0;
            string courseInfo = string.Empty;

            if (Courses != null && Courses.Length != 0)
            {
                for (int i = 0; i < Courses.Length; i++)
                {
                    if (Courses[i] != null)
                    {
                        courseCounter++;
                        courseInfo += Courses[i].ToString();
                    }
                }
            }

            studentDataAsStr += (courseCounter == 0) ? "\nNo Addresses are documented at the moment" :
                              $"\n{courseCounter} addresses are documented:{courseInfo}\n";

            return studentDataAsStr;
        }




    }
}
