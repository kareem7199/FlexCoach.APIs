
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using FlexCoach.APIs.Dtos.Coach.Certificate;
using FlexCoach.APIs.Dtos.Coach.Plan;
using FlexCoach.Core;
using FlexCoach.Core.Entities;
using FlexCoach.Core.Services.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FlexCoach.APIs.Controllers
{
	public class CoachController : BaseApiController
	{
		private readonly IUploadService _uploadService;
		private readonly ICoachService _coachService;
		private readonly IMapper _mapper;

		public CoachController(
			IUploadService uploadService,
			ICoachService coachService,
			IMapper mapper
			)
		{
			_uploadService = uploadService;
			_coachService = coachService;	
			_mapper = mapper;
		}
	}
}
