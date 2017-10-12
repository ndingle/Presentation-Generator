using LinqToExcel.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Day_Generator
{


    public enum StudentSort { Surname, Firstname, Id }


    public class StudentCollection
    {

        private List<Student> m_Students;

        public StudentCollection()
        {
            m_Students = new List<Student>();
        }


        public void Add(StudentRow student, bool addNewStudents)
        {
            if (student != null)
            {

                Student newStudent = student.ToStudent();

                //If student doesn't exist, convert and add, otherwise just add the award
                if (!m_Students.Contains(newStudent) && addNewStudents)
                {
                    m_Students.Add(newStudent);
                }
                else
                {
                    (m_Students[m_Students.IndexOf(newStudent)] as Student).Awards.Add(student.Award);
                }

            }
        }


        public List<Student> ToList(StudentSort sort)
        {
            switch (sort)
            {
                case StudentSort.Id:
                    m_Students.Sort(delegate (Student s1, Student s2) { return s1.Id.CompareTo(s2.Id); });
                    break;
                case StudentSort.Firstname:
                    m_Students.Sort(delegate (Student s1, Student s2) { return String.Compare(s1.Firstname, s2.Firstname); });
                    break;
                case StudentSort.Surname:
                    m_Students.Sort(delegate (Student s1, Student s2) { return String.Compare(s1.Surname, s2.Surname); });
                    break;
            }

            return m_Students;
        }


        public void AddRange(StudentRow[] students, bool addNewStudents)
        {
            foreach (StudentRow student in students)
                Add(student, addNewStudents);
        }


        public void RemoveAt(int index)
        {
            m_Students.RemoveAt(index);
        }

    }


    public class Student
    {

        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public List<string> Awards { get; set; }
        public List<string> Photos { get; set; }

        public Student()
        {
            Id = 0;
            Firstname = "";
            Surname = "";
            Awards = new List<string>();
            Photos = new List<string>();
        }


        public override bool Equals(object obj)
        {
            Student other = obj as Student;
            return (Id == other.Id &&
                    Firstname == other.Firstname &&
                    Surname == other.Surname);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }


    public class StudentRow
    {

        [ExcelColumn("Student Id")]
        public long Id { get; set; }
        [ExcelColumn("First Name")]
        public string Firstname { get; set; }
        [ExcelColumn("Surname")]
        public string Surname { get; set; }
        [ExcelColumn("Award")]
        public string Award { get; set; }

        public Student ToStudent()
        {

            //Setup class and return
            Student student = new Student();
            student.Id = Id;
            student.Firstname = Firstname;
            student.Surname = Surname;
            student.Awards.Add(Award);
            return student;

        }

        public static explicit operator Student(StudentRow obj)
        {
            return obj.ToStudent();
        }

    }


}
