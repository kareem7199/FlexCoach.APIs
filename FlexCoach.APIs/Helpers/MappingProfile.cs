using AutoMapper;
using FlexCoach.APIs.Dtos.Account;
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

			CreateMap<Account, AccountDto>()
                    .ForMember(D => D.ProfilePictureUrl, O => O.MapFrom<AccountProfilePictureUrlResolver>());
        }
    }
}
