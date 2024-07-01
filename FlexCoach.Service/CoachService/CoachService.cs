using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core;
using FlexCoach.Core.Entities;
using FlexCoach.Core.Services.Contract;

namespace FlexCoach.Service.CoachService
{
	public class CoachService : ICoachService
	{
		private readonly IUnitOfWork _unitOfWork;

		public CoachService(IUnitOfWork unitOfWork)
        {
			_unitOfWork = unitOfWork;
		}

		#region Certificate
		public async Task<Certification?> AddCertificate(string certificateUrl, string coachEmail)
		{
			var coachRepo = _unitOfWork.Repository<Coach>();

			var spec = new AccountSpecifications<Coach>(coachEmail);

			var coach = await coachRepo.GetWithSpecAsync(spec);

			if (coach is null)
				return null;

			var certificate = new Certification()
			{
				CertificateUrl = certificateUrl,
				CoachId = coach.Id
			};

			coach.Certifications.Add(certificate);

			await _unitOfWork.CompleteAsync();

			return certificate;
		}

		public async Task<Certification?> DeleteCertificate(int certificateId, string coachEmail)
		{
			var certificateRepo = _unitOfWork.Repository<Certification>();
			var coachRepo = _unitOfWork.Repository<Coach>();

			var spec = new AccountSpecifications<Coach>(coachEmail);
			var coach = await coachRepo.GetWithSpecAsync(spec);

			if (coach is null) return null;

			var certificate = await certificateRepo.GetAsync(certificateId);

			if (certificate is null || (certificate.CoachId != coach.Id)) return null;

			certificateRepo.Delete(certificate);

			await _unitOfWork.CompleteAsync();

			return certificate;
		}
		#endregion

		#region Plan
		public async Task<Plan?> AddPlan(Plan plan, string coachEmail)
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

		public async Task<Plan?> DeletePlan(int planId, string coachEmail)
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

		#endregion
	}
}
