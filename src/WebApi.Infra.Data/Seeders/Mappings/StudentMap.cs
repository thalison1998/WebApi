using CsvHelper.Configuration;

namespace WebApi.Infra.Data.Seeders.Mappings;

public class StudentMap : ClassMap<StudentDto>
{
    public StudentMap()
    {
        Map(m => m.Name).Name("Name");
        Map(m => m.Age).Name("Age");
        Map(m => m.Grade).Name("Grade");
        Map(m => m.AverageGrade).Name("AverageGrade");
        Map(m => m.Address).Name("Address");
        Map(m => m.FatherName).Name("FatherName");
        Map(m => m.MotherName).Name("MotherName");
        Map(m => m.BirthDate).Name("BirthDate");
    }
}