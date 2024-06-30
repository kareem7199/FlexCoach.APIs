using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace FlexCoach.Core.Services.Contract
{
	public interface IAuthService
	{
		Task<string> CreateTokenAsync(Account user);
	}
}
