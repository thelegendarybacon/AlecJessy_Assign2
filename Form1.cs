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
        SortedList<uint, Student> studentPool = new SortedList<uint, Student>();
        List<Course> coursePool = new List<Course>();
        List<string> majors = new List<string>();

        public Form1()
        {
            InitializeComponent();

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

                coursePool.Sort();
            }

            //reads in major data
            path = "input_03.txt";
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        majors.Add(s);
                        comboBox1.Items.Add(s);
                    }
                }
            }

            comboBox2.Items.Add("Freshman");
            comboBox2.Items.Add("Sophmore");
            comboBox2.Items.Add("Junior");
            comboBox2.Items.Add("Senior");
        }

        private void refreshLists(SortedList<uint, Student> s, List<Course> c)
        {
            listBox2.Items.Clear();
            listBox1.Items.Clear();

            IList<Student> stu = s.Values;
            foreach(Student stdnt in stu)
                listBox2.Items.Add(stdnt.ToString("list"));

            foreach (Course crse in c)
                listBox1.Items.Add(crse.ToString());
        }

        // Adds a student to list of students
        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                string name, zid, major, year;
                name = textBox1.Text.ToString();
                zid = textBox2.Text.ToString();
                major = comboBox1.SelectedItem.ToString();
                year = comboBox2.SelectedItem.ToString();
                uint z = Convert.ToUInt32(zid.Substring(1), 10);

                Student tmpStu = new Student(z, name, major, year);
                studentPool.Add(z, tmpStu);
            }
            else
            {
                richTextBox1.Clear();
                richTextBox1.Text = "Error: Must enter data in the 4 fields";
            }
            refreshLists(studentPool, coursePool);
        }

        // Adds a course to list of courses
        private void button6_Click(object sender, EventArgs e)
        {
            string dCode, cNum, sNum;
            dCode = comboBox3.SelectedItem.ToString();
            cNum = textBox6.Text.ToString();
            sNum = textBox5.Text.ToString();
            ushort cap = Convert.ToUInt16(numericUpDown1.Value);

            Course tmpCrse = new Course(dCode, cNum, sNum, cap);
            coursePool.Add(tmpCrse);

            refreshLists(studentPool, coursePool);
        }

        // Enrolls the selected Student into selected Course
        private void button2_Click(object sender, EventArgs e)
        {
            string[] eString = coursePool[listBox1.SelectedIndex].ToString().Split('(');
            richTextBox1.Clear();
            if (listBox1.SelectedIndex >= 0 && listBox2.SelectedIndex >= 0)
            {
                string idGrab = listBox2.SelectedItem.ToString().Substring(1, 7);
                uint zid = Convert.ToUInt32(idGrab, 10);
                int result = studentPool[zid].Enroll(coursePool[listBox1.SelectedIndex]);
                if (result == 5)
                    richTextBox1.Text = "Error: Course is at capacity";
                else if (result == 10)
                    richTextBox1.Text = "Error: Student is already enrolled";
                else if (result == 15)
                    richTextBox1.Text = "Error: Student can not enroll for more than 18 credit hours";
                else
                    richTextBox1.Text = "z" + zid + " has been enrolled in " + eString[0];
            }
            else
            {
                richTextBox1.Text = "Error: Must select a student and a course to enroll";
            }

            refreshLists(studentPool, coursePool);
        }

        // Drops the selected Student from selected Course
        private void button3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0 && listBox2.SelectedIndex >= 0)
            {
                uint zid = Convert.ToUInt32(listBox2.SelectedItem.ToString(), 10);
                int result = studentPool[zid].Drop(coursePool[listBox1.SelectedIndex]);
            }
            else
            {
                richTextBox1.Clear();
                richTextBox1.Text = "Error: Must select a student and a course to drop";
            }

            refreshLists(studentPool, coursePool);
        }
        
        // Prints the selected Course's Roster in bottom box
        private void button1_Click(object sender, EventArgs e)
        {

            if (listBox1.SelectedIndex >= 0)
            {
                Course selected = coursePool[listBox1.SelectedIndex];
                Student curr;
                int counter = 0;
                string output = "Course: " + selected.ToString();
                output += "\n----------------------------------------\n\n";

                if(selected.getSize() > 0)
                {
                    while (counter < selected.getSize())
                    {
                        curr = studentPool[selected.getEnrolled()[counter]];
                        output += curr.ToString("roster");
                        output += "\n";

                        counter++;
                    }
                }

                //format text into columns
                richTextBox1.Clear();
                richTextBox1.Text = output;
                richTextBox1.SelectAll();
                richTextBox1.SelectionTabs = new int[] { 100, 200 };
                richTextBox1.AcceptsTab = true;
                richTextBox1.Select(0, 0);
            }
            else
            {
                richTextBox1.Clear();
                richTextBox1.Text = "Error: Must select a course to display the roster of";
            }
        }

        // Filters the list of Students
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        // Filters the list of Courses
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        // Passive reaction for student selection
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }

    }
}
