using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlecJessy_Assign2
{
    public class CourseEnum : IEnumerator
    {
        public List<uint> students;

        int position = -1;

        public CourseEnum(List<uint> list)
        {
            students = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < students.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public uint? Current
        {
            get
            {
                try
                {
                    return students[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

    }
}
