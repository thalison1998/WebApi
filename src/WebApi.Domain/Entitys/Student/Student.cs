using System.ComponentModel.DataAnnotations;
using WebApi.Domain.Entitys.Base;
using WebApi.Domain.ErrorMessages;

namespace WebApi.Domain.Entitys.Student
{
    public class Student : EntityBase
    {
        public Student() { }

        private Student(string name, int age, int grade, double averageGrade, string address, string fatherName, string motherName, DateTime birthDate)
        {
            Name = name;
            Age = age;
            Grade = grade;
            AverageGrade = averageGrade;
            Address = address;
            FatherName = fatherName;
            MotherName = motherName;
            BirthDate = birthDate;
        }

        private Student(string name, int age, int grade, double averageGrade, string address, string fatherName, string motherName, DateTime birthDate, int id)
        {
            Name = name;
            Age = age;
            Grade = grade;
            AverageGrade = averageGrade;
            Address = address;
            FatherName = fatherName;
            MotherName = motherName;
            BirthDate = birthDate;
            Id = id;
        }

        [Required(ErrorMessage = Error.RequiredField)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.StringLength)]
        public string Name { get; private set; }

        [Range(0, 120, ErrorMessage = Error.Range)]
        public int Age { get; private set; }

        [Range(1, 12, ErrorMessage = Error.Range)]
        public int Grade { get; private set; }

        [Range(0, 10, ErrorMessage = Error.Range)]
        public double AverageGrade { get; private set; }

        [Required(ErrorMessage = Error.RequiredField)]
        [StringLength(250, MinimumLength = 1, ErrorMessage = Error.StringLength)]
        public string Address { get; private set; }

        [Required(ErrorMessage = Error.RequiredField)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.StringLength)]
        public string FatherName { get; private set; }

        [Required(ErrorMessage = Error.RequiredField)]
        [StringLength(100, MinimumLength = 1, ErrorMessage = Error.StringLength)]
        public string MotherName { get; private set; }

        [Required(ErrorMessage = Error.RequiredField)]
        public DateTime BirthDate { get; private set; }

        public static Student Create(string name, int age, int grade, double averageGrade, string address, string fatherName, string motherName, DateTime birthDate)
        {
            var student = new Student(name, age, grade, averageGrade, address, fatherName, motherName, birthDate);
            Validate(student);
            return student;
        }

        public static Student CreateForUpdate(string name, int age, int grade, double averageGrade, string address, string fatherName, string motherName, DateTime birthDate, int id)
        {
            var student = new Student(name, age, grade, averageGrade, address, fatherName, motherName, birthDate, id);
            Validate(student);
            return student;
        }

        public void Update(Student student)
        {
            Name = student.Name;
            Age = student.Age;
            Grade = student.Grade;
            AverageGrade = student.AverageGrade;
            Address = student.Address;
            FatherName = student.FatherName;
            MotherName = student.MotherName;
            BirthDate = student.BirthDate;

            Validate(this);
        }

        private static void Validate(Student student)
        {
            var validationContext = new ValidationContext(student);
            var validationResults = new List<ValidationResult>();

            if (!Validator.TryValidateObject(student, validationContext, validationResults, true))
            {
                var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
                throw new ValidationException(errors);
            }
        }
    }
}
