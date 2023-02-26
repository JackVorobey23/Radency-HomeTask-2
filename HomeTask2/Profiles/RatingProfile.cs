using AutoMapper;
using HomeTask2.DataModel;
using HomeTask2.Dto;

namespace HomeTask2.Profiles
{
    public class RatingProfile : Profile
    {
        public RatingProfile()
        {
            CreateMap<RateDto, Rating>();
        }
    }
}
