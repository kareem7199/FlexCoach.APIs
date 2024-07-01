
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using FlexCoach.APIs.Dtos.Coach.Certificate;
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

		[HttpPost("Certificate")]
		[Authorize(Roles = "Coach")]
		public async Task<ActionResult<CertificateToReturnDto>> UploadCertificate(CertificateDto model)
		{
			var coachEmail = User.FindFirst(ClaimTypes.Email).Value;

			var certificateUrl = await _uploadService.UploadPdf(model.Certificate, "certificates");

			var result = await _coachService.AddCertificate(certificateUrl, coachEmail);

			if (result is null)
				return BadRequest();

			return Ok(_mapper.Map<CertificateToReturnDto>(result));
		}

		[HttpDelete("Certificate/{id}")]
		[Authorize(Roles = "Coach")]
		public async Task<ActionResult> DeleteCertificate(int id)
		{

			var coachEmail = User.FindFirst(ClaimTypes.Email).Value;

			var result = await _coachService.DeleteCertificate(id , coachEmail);

			if(result is null) return NotFound();

			return Ok(_mapper.Map<CertificateToReturnDto>(result));
		}
	}
}
