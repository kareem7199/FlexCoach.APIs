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
	}
}
