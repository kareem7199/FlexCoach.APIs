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

		public AccountController(
			IAuthService authService,
			IAccountService accountService,
			IMapper mapper
			)
		{
			_authService = authService;
			_accountService = accountService;
			_mapper = mapper;
		}

		[HttpPost("Trainee/Register")]
		public async Task<ActionResult> RegisterTrainee(TraineeRegisterDto traineeRegisterDto)
		{

			var trainee = _mapper.Map<Trainee>(traineeRegisterDto);

			var account = await _accountService.RegisterAsync(trainee);

			if (account is null)
				return BadRequest(new ApiResponse(400, "Email already exists."));

			return Ok(new AccountDto()
			{
				Email = trainee.Email,
				Name = trainee.Name,
				Id = trainee.Id,
				Role = "Trainee",
				Token = await _authService.CreateTokenAsync(trainee)
			});
		}

		[HttpPost("Trainee/Login")]
		public async Task<ActionResult<AccountDto>> LoginTrainee(LoginDto model)
		{

			var trainee = await _accountService.LoginAsync<Trainee>(model.Email, model.Password);

			if (trainee is null)
				return Unauthorized(new ApiResponse(401, "invalid login"));

			return Ok(new AccountDto()
			{
				Email = trainee.Email,
				Name = trainee.Name,
				Id = trainee.Id,
				Role = "Trainee",
				Token = await _authService.CreateTokenAsync(trainee)
			});
		}



	}
}
