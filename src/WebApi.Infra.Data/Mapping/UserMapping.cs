using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entitys.User;
using WebApi.Infra.Data.Constants;

namespace WebApi.Infra.Data.Mapping;

public class UserMapping : EntityBaseMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.ToTable("users", Constantes.Schemas.Identity);
    }
}

