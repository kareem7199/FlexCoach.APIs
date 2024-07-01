using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;

namespace FlexCoach.Core.Services.Contract
{
	public interface ICoachService
	{
		public Task<Certification?> AddCertificate(string certificateUrl , string coachEmail);
		public Task<Certification?> DeleteCertificate(int certificateId , string coachEmail);
	}
}
