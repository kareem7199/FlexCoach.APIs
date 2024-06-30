using AutoMapper;
using FlexCoach.APIs.Dtos.Account;
using FlexCoach.Core.Entities;

namespace FlexCoach.APIs.Helpers.Resolvers
{
	public class AccountProfilePictureUrlResolver : IValueResolver<Account , AccountDto , string>
	{
		private readonly IConfiguration _configuration;

		public AccountProfilePictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Account source, AccountDto destination, string destMember, ResolutionContext context)
		{
			return $"{_configuration["ApiBaseUrl"]}/profilePictures/{source.PictureUrl}";
		}
	}
}
