using WebApi.Domain.Entitys.Base;

namespace WebApi.Domain.Entitys.Student;
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

    public string Name { get; private set; }
    public int Age { get; private set; }
    public int Grade { get; private set; }
    public double AverageGrade { get; private set; }
    public string Address { get; private set; }
    public string FatherName { get; private set; }
    public string MotherName { get; private set; }
    public DateTime BirthDate { get; private set; }

    public static Student Create(string name, int age, int grade, double averageGrade, string address, string fatherName, string motherName, DateTime birthDate)
    {
        return new Student(name, age, grade, averageGrade, address, fatherName, motherName, birthDate);
    }
}
