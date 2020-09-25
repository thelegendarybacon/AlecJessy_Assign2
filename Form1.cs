using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlecJessy_Assign2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SortedList<uint, Student> studentPool = new SortedList<uint, Student>();
            List<Course> coursePool = new List<Course>();

            string s;

            //reads in student data
            string path = "input_01.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        Student stdnt = new Student(s);
                        studentPool.Add(stdnt.getID(), stdnt);
                        listBox2.Items.Add(stdnt.ToString("list"));
                    }
                }
            }

            //reads in course data
            path = "input_02.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        Course c = new Course(s);
                        coursePool.Add(c);
                        listBox1.Items.Add(c.ToString());
                    }
                }
            }


        }

        // Adds a student to list of students
        private void button5_Click(object sender, EventArgs e)
        {

        }

        // Adds a course to list of courses
        private void button6_Click(object sender, EventArgs e)
        {

        }

        // Selection of a Student to enroll/drop
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Selection of a Course to enroll/drop Student from
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        // Enrolls the selected Student into selected Course
        private void button2_Click(object sender, EventArgs e)
        {

        }

        // Drops the selected Student from selected Course
        private void button3_Click(object sender, EventArgs e)
        {

        }
        
        // Prints the selected Course's Roster in bottom box
        private void button1_Click(object sender, EventArgs e)
        {

        }

        // Filters the list of Students
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // Filters the list of Courses
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
