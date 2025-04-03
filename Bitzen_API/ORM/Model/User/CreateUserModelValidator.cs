using FluentValidation;
namespace Bitzen_API.ORM.Model.User
{
    public class CreateUserModelValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Nome é obrigatório.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email inválido.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Senha deve ter no mínimo  caracteres.");
        }
    }
}
