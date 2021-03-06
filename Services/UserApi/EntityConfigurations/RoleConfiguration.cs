﻿//-----------------------------------------------------------------------
// <copyright file="RoleConfiguration.cs" website="Patrikduch.com">
//     Copyright 2019 (c) Patrikduch.com
// </copyright>
// <author>Patrik Duch</author>
//-----------------------------------------------------------------------

namespace UserApi.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Microsoft.EntityFrameworkCore;
    using PersistenceLib.Domains.UserApi;

    /// <summary>
    /// Entity configuration for Role entity
    /// </summary>
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            // Table
            builder.ToTable("Role");
        }
    }
}
