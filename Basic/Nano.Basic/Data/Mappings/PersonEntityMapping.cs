using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Basic.Models;
using Nano.Basic.Models.Enums;
using Nano.Data.Models.Mappings;
using Nano.Data.Models.Mappings.Extensions;

namespace Nano.Basic.Data.Mappings
{
    /// <inheritdoc />
    public class PersonEntityMapping : DefaultEntityMapping<Person>
    {
        /// <inheritdoc />
        public override void Map(EntityTypeBuilder<Person> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            base.Map(builder);

            builder
                .Property(x => x.Name)
                .IsRequired();

            builder
                .HasIndex(x => x.Name)
                .IsUnique();

            builder
                .Property(x => x.Gender)
                .HasDefaultValue(Gender.Undefined)
                .IsRequired();

            builder
                .Property(x => x.IsFriendly)
                .HasDefaultValue(true)
                .IsRequired();

            builder
                .MapType(x => x.PhoneNumber);

            builder
                .MapType(x => x.EmailAddress);
        }
    }
}