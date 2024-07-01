using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using FlexCoach.Core;
using FlexCoach.Core.Services.Contract;

namespace FlexCoach.Service.PlanService
{
	public class PlanService : IPlanService
	{
		private readonly IUnitOfWork _unitOfWork;

		public PlanService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<Plan?> AddPlanAsync(Plan plan, string coachEmail)
		{
			var coachRepo = _unitOfWork.Repository<Coach>();
			var planRepo = _unitOfWork.Repository<Plan>();

			var spec = new AccountSpecifications<Coach>(coachEmail);
			var coach = await coachRepo.GetWithSpecAsync(spec);

			if (coach is null)
				return null;

			plan.CoachId = coach.Id;

			planRepo.Add(plan);
			await _unitOfWork.CompleteAsync();

			return plan;
		}

		public async Task<Plan?> DeletePlanAsync(int planId, string coachEmail)
		{
			var coachRepo = _unitOfWork.Repository<Coach>();
			var planRepo = _unitOfWork.Repository<Plan>();

			var spec = new AccountSpecifications<Coach>(coachEmail);
			var coach = await coachRepo.GetWithSpecAsync(spec);
			if (coach is null)
				return null;

			var plan = await planRepo.GetAsync(planId);
			if (plan is null || plan.IsDeleted || (plan.CoachId != coach.Id)) return null;

			plan.IsDeleted = true;

			planRepo.Update(plan);
			await _unitOfWork.CompleteAsync();

			return plan;
		}
	}
}
