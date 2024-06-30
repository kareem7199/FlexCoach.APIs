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
	internal class CoachConfigurations : IEntityTypeConfiguration<Coach>
	{
		public void Configure(EntityTypeBuilder<Coach> builder)
		{
			builder.HasIndex(C => C.Email)
				   .IsUnique();

			builder.Property(C => C.Gender)
				   .HasConversion(
					(CGender) => CGender.ToString(),
					(CGender) => (Gender)Enum.Parse(typeof(Gender), CGender)
					);
		}
	}
}
