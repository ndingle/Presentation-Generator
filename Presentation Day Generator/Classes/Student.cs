using LinqToExcel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Day_Generator
{

    public class StudentCollection
    {

        private List<Student> m_Students;


        public void Add(StudentRow student)
        {
            if (student != null)
            {

                //Build a student
                Student newStudent = new Student(student);

                //If student doesn't exist, convert and add, otherwise just add the award
                if (!m_Students.Contains(newStudent))
                {
                    m_Students.Add(newStudent);
                }
                else
                {
                    (m_Students[m_Students.IndexOf(newStudent)] as Student).Awards.Add(student.Award);
                }

            }
        }


        public void RemoveAt(int index)
        {
            m_Students.RemoveAt(index);
        }

    }


    public class Student
    {

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public List<string> Awards { get; set; }
        public List<string> Photos { get; set; }

        public Student()
        {
            Id = 0;
            FirstName = "";
            Surname = "";
            Awards = new List<string>();
            Photos = new List<string>();
        }

        public Student(StudentRow student)
        {
            Id = student.Id;
            FirstName = student.FirstName;
            Surname = student.Surname;
            Awards.Add (student.Award);
        }

        public override bool Equals(object obj)
        {
            Student other = obj as Student;
            return (Id == other.Id &&
                    FirstName == other.FirstName &&
                    Surname == other.Surname);
        }

    }


    public class StudentRow
    {

        [ExcelColumn("Student Id")]
        public long Id { get; set; }
        [ExcelColumn("First Name")]
        public string FirstName { get; set; }
        [ExcelColumn("Surname")]
        public string Surname { get; set; }
        [ExcelColumn("Award")]
        public string Award { get; set; }

    }


}
