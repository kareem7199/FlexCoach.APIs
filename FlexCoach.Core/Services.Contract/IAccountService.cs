using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;

namespace FlexCoach.Core.Services.Contract
{
	public interface IAccountService
	{
		public Task<Account?> LoginAsync<T>(string Email, string Password) where T : Account;
		public Task<Account?> RegisterAsync<T>(T account) where T : Account;
	}
}
