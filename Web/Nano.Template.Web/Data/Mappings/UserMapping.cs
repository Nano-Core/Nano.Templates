﻿using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nano.Data.Models.Mappings;
using Nano.Template.Web.Models;

namespace Nano.Template.Web.Data.Mappings
{
    /// <inheritdoc />
    public class UserMapping : DefaultEntityUserMapping<User>
    {
        /// <inheritdoc />
        public override void Map(EntityTypeBuilder<User> builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));

            base.Map(builder);

            builder
                .Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            builder
                .HasIndex(x => x.Name);
        }
    }
}