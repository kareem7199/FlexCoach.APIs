using FlexCoach.APIs.Dtos.Coach.Certificate;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlexCoach.Core.Services.Contract;
using AutoMapper;

namespace FlexCoach.APIs.Controllers
{
	public class CertificateController : BaseApiController
	{
		private readonly IUploadService _uploadService;
		private readonly IMapper _mapper;
		private readonly ICertificateService _certificateService;

		public CertificateController(
			IUploadService uploadService,
			IMapper mapper,
			ICertificateService certificateService)
		{
			_uploadService = uploadService;
			_mapper = mapper;
			_certificateService = certificateService;
		}

		[HttpPost]
		[Authorize(Roles = "Coach")]
		public async Task<ActionResult<CertificateToReturnDto>> UploadCertificate(CertificateDto model)
		{
			var coachEmail = User.FindFirst(ClaimTypes.Email).Value;

			var certificateUrl = await _uploadService.UploadPdf(model.Certificate, "certificates");

			var result = await _certificateService.AddCertificate(certificateUrl, coachEmail);

			if (result is null)
				return BadRequest();

			return Ok(_mapper.Map<CertificateToReturnDto>(result));
		}

		[HttpDelete("{id}")]
		[Authorize(Roles = "Coach")]
		public async Task<ActionResult> DeleteCertificate(int id)
		{

			var coachEmail = User.FindFirst(ClaimTypes.Email).Value;

			var result = await _certificateService.DeleteCertificate(id, coachEmail);

			if (result is null) return NotFound();

			return Ok(_mapper.Map<CertificateToReturnDto>(result));
		}
	}
}
