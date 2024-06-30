using AutoMapper;
using FlexCoach.APIs.Dtos.Register;
using FlexCoach.Core.Entities;

namespace FlexCoach.APIs.Helpers
{
	public class MappingProfile : Profile
	{
        public MappingProfile()
        {
            CreateMap<TraineeRegisterDto, Trainee>();
        }
    }
}
