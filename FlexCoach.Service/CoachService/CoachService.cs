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

			if(coach is null) return null;
			
			var certificate = await certificateRepo.GetAsync(certificateId);

			if(certificate is null || (certificate.CoachId != coach.Id)) return null;

			certificateRepo.Delete(certificate);

			await _unitOfWork.CompleteAsync();

			return certificate;
		}
	}
}
