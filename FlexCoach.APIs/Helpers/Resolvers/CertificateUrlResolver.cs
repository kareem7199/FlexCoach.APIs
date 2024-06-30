using AutoMapper;
using FlexCoach.APIs.Dtos.Coach.Certificate;
using FlexCoach.Core.Entities;

namespace FlexCoach.APIs.Helpers.Resolvers
{
	public class CertificateUrlResolver : IValueResolver<Certification, CertificateToReturnDto, string>
	{
		private readonly IConfiguration _configuration;

		public CertificateUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Certification source, CertificateToReturnDto destination, string destMember, ResolutionContext context)
		{
			return $"{_configuration["ApiBaseUrl"]}/certificates/{source.CertificateUrl}";
		}
	}
}
