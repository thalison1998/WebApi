﻿using System.Diagnostics;
using System.Net;
using System.Xml.Linq;
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

    public static Student CreateForUpdate(string name, int age, int grade, double averageGrade, string address, string fatherName, string motherName, DateTime birthDate, int id)
    {
        return new Student(name, age, grade, averageGrade, address, fatherName, motherName, birthDate, id);
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
    }
}
