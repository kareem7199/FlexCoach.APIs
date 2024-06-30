using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core;
using FlexCoach.Core.Entities;
using FlexCoach.Core.Services.Contract;

namespace FlexCoach.Service.LoginService
{
	public class AccountService : IAccountService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAuthService _authService;
		private readonly IHashService _hashService;

		public AccountService(
			IUnitOfWork unitOfWork,
			IAuthService authService,
			IHashService hashService)
		{
			_unitOfWork = unitOfWork;
			_authService = authService;
			_hashService = hashService;
		}

		public async Task<Account?> LoginAsync <T>(string Email, string Password) where T : Account
		{
			var Repository = _unitOfWork.Repository<T>();

			var accountSpec = new AccountSpecifications<T>(Email);
			var account = await Repository.GetWithSpecAsync(accountSpec);

			if (account is null || !_hashService.VerifyPassword(Password, account.Password))
				return null;

			return account;
		}

		public async Task<Account?> RegisterAsync<T>(T account) where T : Account
		{
			var repository = _unitOfWork.Repository<T>();

			var accountSpec = new AccountSpecifications<T>(account.Email);
			var isExisted = (await repository.GetWithSpecAsync(accountSpec)) is not null;

			if (isExisted) return null;

			account.Password = _hashService.HashPassword(account.Password);

			repository.Add(account);
			await _unitOfWork.CompleteAsync();

			return account;
		}
	}
}
