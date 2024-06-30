using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexCoach.Repository.Data.Config.Account_Config
{
	internal class AdminConfigurations : IEntityTypeConfiguration<Admin>
	{
		public void Configure(EntityTypeBuilder<Admin> builder)
		{
			builder.HasIndex(T => T.Email)
				   .IsUnique();

			builder.Property(A => A.Gender)
				   .HasConversion(
					(AGender) => AGender.ToString(),
					(AGender) => (Gender)Enum.Parse(typeof(Gender), AGender)
					);
		}
	}
}
