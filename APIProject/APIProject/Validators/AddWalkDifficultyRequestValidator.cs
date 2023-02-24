using FluentValidation;

namespace APIProject.Validators
{
    public class AddWalkDifficultyRequestValidator: AbstractValidator<Models.DTO.AddWalkDifficulttyRequest>
    {
        public AddWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
