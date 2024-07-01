using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;

namespace FlexCoach.Core.Services.Contract
{
	public interface IPlanService
	{
		public Task<Plan?> AddPlanAsync(Plan plan, string coachEmail);
		public Task<Plan?> DeletePlanAsync(int planId, string coachEmail);
	}
}
