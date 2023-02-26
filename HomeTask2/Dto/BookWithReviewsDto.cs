using FluentValidation;
using HomeTask2.DataModel;

namespace HomeTask2.Dto
{
    public class BookWithReviewsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Cover { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public decimal Rating { get; set; }
        public IEnumerable<NoBookIdReviewDto> Reviews { get; set; }
    }
    public class BookWithReviewValidator : FluentValidation.AbstractValidator<BookWithReviewsDto>
    {
        public BookWithReviewValidator()
        {
            RuleFor(bwr => bwr.Author).MinimumLength(3);
            RuleFor(bwr => bwr.Title).MinimumLength(2);
            RuleFor(bwr => bwr.Content).MinimumLength(5);
            RuleFor(bwr => bwr.Cover).MinimumLength(3);
        }
    }
}
