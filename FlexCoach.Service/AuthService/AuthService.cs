using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Entities;
using FlexCoach.Core.Services.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FlexCoach.Service.AuthService
{
	public class AuthService : IAuthService
	{
		private readonly IConfiguration _configuration;

		public AuthService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public async Task<string> CreateTokenAsync(Account user)
		{

			string role;
			if (user is Admin) role = "Admin";
			else if (user is Coach) role = "Coach";
			else role = "Trainee";

			// Private Claims
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email , user.Email) ,
				new Claim(ClaimTypes.Role, role)
			};

			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:AuthKey"] ?? string.Empty));

			var token = new JwtSecurityToken(

				audience: _configuration["JWT:ValidAudience"],
				issuer: _configuration["JWT:ValidIssuer"],
				expires: DateTime.Now.AddDays(Double.Parse(_configuration["JWT:DurationInDays"] ?? "0")),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)

				);

			return new JwtSecurityTokenHandler().WriteToken(token);

		}
	}
}
