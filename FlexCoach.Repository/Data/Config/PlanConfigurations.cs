using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlexCoach.Repository.Data.Config
{
	public class PlanConfigurations : IEntityTypeConfiguration<Plan>
	{
		public void Configure(EntityTypeBuilder<Plan> builder)
		{
			builder.Property(P => P.IsDeleted)
				   .HasDefaultValue(false);
		}
	}
}
