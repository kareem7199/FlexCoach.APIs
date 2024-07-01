using FlexCoach.APIs.Dtos.Coach.Plan;
using FlexCoach.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FlexCoach.Core.Services.Contract;

namespace FlexCoach.APIs.Controllers
{
	public class PlanController : BaseApiController
	{
		private readonly IUploadService _uploadService;
		private readonly IMapper _mapper;
		private readonly IPlanService _planService;

		public PlanController(
			IUploadService uploadService,
			IMapper mapper,
			IPlanService planService)
		{
			_uploadService = uploadService;
			_mapper = mapper;
			_planService = planService;
		}

		[HttpPost]
		[Authorize(Roles = "Coach")]
		public async Task<ActionResult<Plan>> AddPlan(PlanDto model)
		{
			var coachEmail = User.FindFirst(ClaimTypes.Email).Value;

			var mappedPlan = _mapper.Map<Plan>(model);

			var plan = await _planService.AddPlanAsync(mappedPlan, coachEmail);

			if (plan is null) return BadRequest();

			return Ok(plan);
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Coach")]
		public async Task<ActionResult<Plan>> DeletePlan(int id)
		{
			var coachEmail = User.FindFirst(ClaimTypes.Email).Value;

			var plan = await _planService.DeletePlanAsync(id, coachEmail);

			if (plan is null) return NotFound();

			return Ok(plan);
		}
		
	}
}
