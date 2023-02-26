using FluentValidation;

namespace HomeTask2.Dto
{
    public class RateDto
    {
        public int Score { get; set; }
    }
    public class RateDtoValidator : FluentValidation.AbstractValidator<RateDto>
    {
        public RateDtoValidator()
        {
            RuleFor(rd => rd.Score).InclusiveBetween(1, 5);
        }
    }
}
