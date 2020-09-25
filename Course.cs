using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlecJessy_Assign2
{
    public class Course : IComparable, IEnumerable
    {
        string dep;
        uint cnum;
        string section;
        ushort cHours;
        List<uint> students;
        ushort csize;
        ushort maxcap;

        //Default Constructor
        public Course()
        {
            dep = null;
            cnum = 0;
            section = null;
            cHours = 0;
            students = null;
            csize = 0;
            maxcap = 0;
        }

        //Takes line of data and splits it to store in class vars
        public Course(string data)
        {
            string[] d = data.Split(',');
            dep = d[0];
            cnum = Convert.ToUInt32(d[1], 10);
            section = d[2];
            cHours = Convert.ToUInt16(d[3]);
            maxcap = Convert.ToUInt16(d[4]);
            csize = 0;
            students = new List<uint>();
        }

        //Implementation of CompareTo
        public int CompareTo(object alpha)
        {

            if (alpha == null) throw new ArgumentNullException();

            Course right = alpha as Course;

            if (right != null)
                if (dep.CompareTo(right.dep) == 0)
                    return cnum.CompareTo(right.cnum);
                else
                    return dep.CompareTo(right.dep);
            else
                throw new ArgumentException("[Course]: Argument is not a Course");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public CourseEnum GetEnumerator()
        {
            return new CourseEnum(students);
        }

        //Overrides ToString to output Course data
        public override string ToString()
        {
            return dep + " " + cnum + "-" + section + " (" + csize + "/" + maxcap + ")";
        }

        //Compares the class size to the class capacity
        public bool atCap()
        {
            if (csize >= maxcap)
                return true;
            else
                return false;
        }

        //Removes student from List students and decrements class size by 1
        public bool remove(uint id)
        {
            csize--;
            return students.Remove(id);
        }

        //Adds student to List students and increments class size by 1
        public void add(uint id)
        {
            students.Add(id);
            csize++;
        }

        //Looks for a given zid in List students
        public bool find(uint id)
        {
            return students.Contains(id);
        }

        // Below are getter methods
        public List<uint> getEnrolled()
        {
            return students;
        }

        public ushort getHours()
        {
            return cHours;
        }

        public string getDept()
        {
            return dep;
        }

        public uint getCNum()
        {
            return cnum;
        }

        public string getSection()
        {
            return section;
        }

        public ushort getSize()
        {
            return csize;
        }

        public ushort getMax()
        {
            return maxcap;
        }
    }
}
