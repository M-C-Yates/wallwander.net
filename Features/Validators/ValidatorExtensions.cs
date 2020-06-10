using FluentValidation;

namespace Features.Validators
{
  public static class ValidatorExtensions
  {
    public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      var options = ruleBuilder
      .NotEmpty()
      .MinimumLength(8).WithMessage("Minimum password length is 8")
      .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter")
      .Matches("[a-z]").WithMessage("Password must contain at least one lower case letter")
      .Matches("[0-9]").WithMessage("Password must contain at least one number")
      .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non alphanumberic character");

      return options;
    }
  }
}