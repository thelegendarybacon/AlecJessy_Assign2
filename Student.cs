using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlecJessy_Assign2
{
    public class Student : IComparable
    {
        //class variables
        readonly uint zid;
        string fname;
        string lname;
        string major;
        enum Year { None, Freshman, Sophmore, Junior, Senior, PostBacc };
        Year grade;
        float? GPA;
        ushort? hours;

        //default constructor
        public Student()
        {
            fname = null;
            lname = null;
            major = null;
            grade = Year.None;
            GPA = null;
            hours = null;
        }

        //Overloaded Constructor
        public Student(string data)
        {
            string[] dataSplit = data.Split(',');                   //splits data by the commas
            zid = Convert.ToUInt32(dataSplit[0], 10);
            fname = dataSplit[1];
            lname = dataSplit[2];
            major = dataSplit[3];
            grade = Year.None;

            //determines year from the number read from file
            switch (dataSplit[4])
            {
                case "0":
                    grade = Year.Freshman;
                    break;
                case "1":
                    grade = Year.Sophmore;
                    break;
                case "2":
                    grade = Year.Junior;
                    break;
                case "3":
                    grade = Year.Senior;
                    break;
                case "4":
                    grade = Year.PostBacc;
                    break;
                default:
                    break;
            }
            GPA = float.Parse(dataSplit[5], CultureInfo.InvariantCulture.NumberFormat);
            hours = 0;
        }

        // Second overloaded constructor, data from all string variables
        public Student(uint z, string n, string m, string g)
        {
            string[] nameSplit = n.Split(',');
            fname = nameSplit[1];
            lname = nameSplit[0];
            major = m;
            GPA = 0;
            hours = 0;
            zid = z;

            Year tmp;
            if (Enum.TryParse(g, out tmp))
                grade = tmp;
            else
                grade = Year.None;
        }

        //implements CompareTo from Comparable
        public int CompareTo(object alpha)
        {
            if (alpha == null) throw new ArgumentNullException();

            Student right = alpha as Student;

            if (right != null)
                return zid.CompareTo(right.zid);
            else
                throw new ArgumentException("[Student]: Argument is not a Student");
        }

        //Enrolls a student if the conditions are met
        public int Enroll(Course newCourse)
        {
            if (newCourse.atCap())                       //if at max capactiy, returns error code 5
                return 5;

            if (newCourse.find(zid))                     //if already enrolled, returns error code 10
                return 10;

            if ((hours + newCourse.getHours()) > 18)     //if student has too many hours, returns error code 15
                return 15;

            newCourse.add(zid);
            hours += newCourse.getHours();
            return 0;
        }

        //Used to drop the student from the given course after checking if they're enrolled already
        public int Drop(Course newCourse)
        {
            if (newCourse.remove(zid))                   //removes student if they're enrolled
                return 0;
            else
                return 20;                              //returns error code if student isn't in the class
        }

        //Overrides ToStrin() method, 'default' and 'roster' are the 2 formats available 
        public string ToString(string type)
        {
            string output = "";
            string name = lname + ", " + fname;
            if (type.Equals("default"))
            {
                output = zid + " --\t" + String.Format("{0,-25}", name);
                output += "[" + String.Format("{0,8}", grade) + "] <" + major;
                output += "> | " + String.Format("{0:.000}", GPA) + " |";
            }
            else if (type.Equals("roster"))
            {
                output = zid + "\t" + String.Format("{0,-25}", name) + "\t" + major;
            }
            else if(type.Equals("list"))
            {
                output = "z" + zid + " -- " + name;
            }

            return output;
        }

        //Compares the students year to the given string
        public bool compareYear(string compare)
        {
            Year y;
            if (Enum.TryParse(compare, out y))
            {
                if (y == grade)
                    return true;
            }
            return false;
        }

        //  Below are get methods for class vars

        public uint getID()
        {
            return zid;
        }

        public string getMajor()
        {
            return major;
        }
    }
}
