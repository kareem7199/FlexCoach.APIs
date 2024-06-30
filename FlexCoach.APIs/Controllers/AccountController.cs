using System.Security.Cryptography;
using AutoMapper;
using FlexCoach.APIs.Dtos.Account;
using FlexCoach.APIs.Dtos.Login;
using FlexCoach.APIs.Dtos.Register;
using FlexCoach.APIs.Errors;
using FlexCoach.Core;
using FlexCoach.Core.Entities;
using FlexCoach.Core.Services.Contract;
using Microsoft.AspNetCore.Mvc;

namespace FlexCoach.APIs.Controllers
{
	public class AccountController : BaseApiController
	{
		private readonly IAuthService _authService;
		private readonly IAccountService _accountService;
		private readonly IMapper _mapper;
		private readonly IUploadService _UploadService;

		public AccountController(
			IAuthService authService,
			IAccountService accountService,
			IMapper mapper,
			IUploadService uploadService
			)
		{
			_authService = authService;
			_accountService = accountService;
			_mapper = mapper;
			_UploadService = uploadService;
		}

		[HttpPost("Trainee/Register")]
		public async Task<ActionResult> RegisterTrainee(TraineeRegisterDto traineeRegisterDto)
		{
			var profilePictureUrl = await _UploadService.UploadFile(traineeRegisterDto.Picture, "profilePictures");

			var trainee = _mapper.Map<Trainee>(traineeRegisterDto);
			trainee.PictureUrl = profilePictureUrl;

			var result = await _accountService.RegisterAsync(trainee);

			if (result is null)
				return BadRequest(new ApiResponse(400, "Email already exists."));

			var token = await _authService.CreateTokenAsync(trainee);
			
			var account = _mapper.Map<AccountDto>(result);
			account.Token = token;
			account.Role = "Trainee";

			return Ok(account);
		}

		[HttpPost("Coach/Register")]
		public async Task<ActionResult> RegisterCoach(CoachRegisterDto coachRegisterDto)
		{
			var profilePictureUrl = await _UploadService.UploadFile(coachRegisterDto.Picture, "profilePictures");

			var coach = _mapper.Map<Coach>(coachRegisterDto);
			coach.PictureUrl = profilePictureUrl;

			var result = await _accountService.RegisterAsync(coach);

			if (result is null)
				return BadRequest(new ApiResponse(400, "Email already exists."));

			var token = await _authService.CreateTokenAsync(coach);

			var account = _mapper.Map<AccountDto>(result);
			account.Token = token;
			account.Role = "Coach";

			return Ok(account);
		}

		[HttpPost("Trainee/Login")]
		public async Task<ActionResult<AccountDto>> LoginTrainee(LoginDto model)
		{
			var trainee = await _accountService.LoginAsync<Trainee>(model.Email, model.Password);

			if (trainee is null)
				return Unauthorized(new ApiResponse(401, "invalid login"));

			var token = await _authService.CreateTokenAsync(trainee);

			var account = _mapper.Map<AccountDto>(trainee);
			account.Token = token;
			account.Role = "Trainee";

			return Ok(account);
		}

		[HttpPost("Coach/Login")]
		public async Task<ActionResult<AccountDto>> LoginCoach(LoginDto model)
		{
			var coach = await _accountService.LoginAsync<Coach>(model.Email, model.Password);

			if (coach is null)
				return Unauthorized(new ApiResponse(401, "invalid login"));

			var token = await _authService.CreateTokenAsync(coach);

			var account = _mapper.Map<AccountDto>(coach);
			account.Token = token;
			account.Role = "Coach";

			return Ok(account);
		}

		[HttpPost("Admin/Login")]
		public async Task<ActionResult<AccountDto>> LoginAdmin(LoginDto model)
		{
			var admin = await _accountService.LoginAsync<Admin>(model.Email, model.Password);

			if (admin is null)
				return Unauthorized(new ApiResponse(401, "invalid login"));

			var token = await _authService.CreateTokenAsync(admin);

			var account = _mapper.Map<AccountDto>(admin);
			account.Token = token;
			account.Role = "Admin";

			return Ok(account);
		}



	}
}
