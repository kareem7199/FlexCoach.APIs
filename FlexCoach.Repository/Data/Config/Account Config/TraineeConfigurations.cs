﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexCoach.Repository.Data.Config.Account_Config
{
	internal class TraineeConfigurations : IEntityTypeConfiguration<Trainee>
	{
		public void Configure(EntityTypeBuilder<Trainee> builder)
		{
			builder.HasIndex(T => T.Email)
				   .IsUnique();

			builder.Property(T => T.Gender)
				   .HasConversion(
					(TGender) => TGender.ToString(),
					(TGender) => (Gender)Enum.Parse(typeof(Gender), TGender)
					);
		}
	}
}
