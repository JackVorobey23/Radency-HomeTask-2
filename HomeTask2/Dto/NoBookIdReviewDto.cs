using FluentValidation;

namespace HomeTask2.Dto
{
    public class NoBookIdReviewDto
    {
        public int Id { get; set; }
        public string Reviewer { get; set; }
        public string Message { get; set; }
    }
    public class NoBookIdReviewDtoValidator : FluentValidation.AbstractValidator<NoBookIdReviewDto>
    {
        public NoBookIdReviewDtoValidator()
        {
            RuleFor(noBookReview => noBookReview.Reviewer).NotEmpty();
        }
    }
}
