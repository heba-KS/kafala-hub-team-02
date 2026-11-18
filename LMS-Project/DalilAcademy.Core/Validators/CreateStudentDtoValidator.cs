using DalilAcademy.Core.DTOs.Students;
using FluentValidation;

namespace DalilAcademy.Core.Validators;

// Lab 6: required name, valid email, max lengths (match StudentConfiguration)
public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
{
    public CreateStudentDtoValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(254);
    }
}
