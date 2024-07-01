using AutoMapper;
using FlexCoach.APIs.Dtos.Account;
using FlexCoach.APIs.Dtos.Coach.Certificate;
using FlexCoach.APIs.Dtos.Coach.Plan;
using FlexCoach.APIs.Dtos.Register;
using FlexCoach.APIs.Helpers.Resolvers;
using FlexCoach.Core.Entities;

namespace FlexCoach.APIs.Helpers
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<TraineeRegisterDto, Trainee>();
			CreateMap<CoachRegisterDto, Coach>();

            CreateMap<PlanDto, Plan>();

			CreateMap<Account, AccountDto>()
                    .ForMember(D => D.ProfilePictureUrl, O => O.MapFrom<AccountProfilePictureUrlResolver>());

            CreateMap<Certification, CertificateToReturnDto>()
                    .ForMember(D => D.CertificateUrl , O => O.MapFrom<CertificateUrlResolver>());
        }
    }
}
