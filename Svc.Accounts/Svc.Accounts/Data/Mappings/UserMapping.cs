using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Mappings.Identity;
using Svc.Accounts.Models.Data;
using Svc.Accounts.Models.Data.Enums;

namespace Svc.Accounts.Data.Mappings;

/// <inheritdoc />
public class UserMapping : BaseEntityUserMapping<User>
{
    /// <inheritdoc />
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        if (builder == null)
            throw new ArgumentNullException(nameof(builder));

        base.Configure(builder);

        builder
            .HasOne(x => x.Tenant)
            .WithMany(x => x.Users)
            .IsRequired();

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasMaxLength(128)
            .IsRequired();

        builder
            .Property(x => x.FullName)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .Property(x => x.FullNameNormalized)
            .HasMaxLength(256)
            .IsRequired();

        builder
            .HasIndex(x => x.FullNameNormalized);

        builder
            .HasOne(x => x.Address)
            .WithOne();
            
        builder
            .Property(x => x.DateOfBirth)
            .IsRequired();

        builder
            .HasIndex(x => x.DateOfBirth);

        builder
            .Ignore(x => x.Age);

        builder
            .Property(x => x.Gender)
            .HasDefaultValue(Gender.Male)
            .IsRequired();

        builder
            .HasIndex(x => x.Gender);

        builder
            .Property(x => x.Language)
            .HasDefaultValue(Language.English)
            .IsRequired();

        builder
            .HasIndex(x => x.Language);

        builder
            .Property(x => x.UserPictureExtension)
            .HasMaxLength(32);
    }
}