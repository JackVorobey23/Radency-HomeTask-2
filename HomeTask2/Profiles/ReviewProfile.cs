using AutoMapper;
using HomeTask2.DataModel;
using HomeTask2.Dto;
namespace HomeTask2.Profiles
{
    public class ReviewProfile: Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review, NoBookIdReviewDto>();
            CreateMap<SaveReviewDto, Review>();
            CreateMap<Review, IdDto>();
        }
    }
}
