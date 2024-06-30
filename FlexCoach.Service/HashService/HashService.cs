using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlexCoach.Core.Services.Contract;
using BCrypt.Net;

namespace FlexCoach.Service.HashService
{
	public class HashService : IHashService
	{
		public string HashPassword(string password)
		{
			return BCrypt.Net.BCrypt.HashPassword(password);
		}

		public bool VerifyPassword(string password, string hashedPassword)
		{
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}
	}
}
