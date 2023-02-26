using AutoMapper;
using HomeTask2.DataModel;
using HomeTask2.Dto;
using System.Runtime.InteropServices;

namespace HomeTask2.Profiles
{
    public class BookProfile: Profile
    {
        public BookProfile() 
        {
            var getRating = (Book b) =>
            {
                if (b.Ratings is not null)
                    return b.Ratings.Sum(r => r.Score) / b.Ratings.Count;
                else
                    return 1;
            };

            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(b => getRating(b)))
                .ForMember(dest => dest.ReviewsNumber, opt => opt.MapFrom(b => b.Reviews.Count));

            CreateMap<Book, BookWithReviewsDto>()
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(b => getRating(b)));


            CreateMap<BookSaveDto, Book>();


            CreateMap<Book, IdDto>();
        }
    }
}
