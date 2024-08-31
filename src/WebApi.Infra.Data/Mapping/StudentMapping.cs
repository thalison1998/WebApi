﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entitys.Student;
using WebApi.Infra.Data.Constants;

namespace WebApi.Infra.Data.Mapping;

public class StudentMapping : EntityBaseMapping<Student>
{
    public override void Configure(EntityTypeBuilder<Student> builder)
    {
        base.Configure(builder);

        builder.ToTable("students", Constantes.Schemas.Sistema);
    }
}