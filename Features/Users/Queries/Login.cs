using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Features.Errors;
using Features.Interfaces;
using Features.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Features.Users.Queries
{
  public class Login
  {
    public class Query : IRequest<User>
    {
      public string Email { get; set; }
      public string Password { get; set; }
    }

    public class QueryValidator : AbstractValidator<Query>
    {
      public QueryValidator()
      {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().Password();
      }
    }

    public class Handler : IRequestHandler<Query, User>
    {
      private readonly UserManager<AppUser> _userManager;
      private readonly SignInManager<AppUser> _signInManager;
      private readonly IJwtGenerator _jwtGenerator;

      public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtGenerator jwtGenerator)
      {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._jwtGenerator = jwtGenerator;
      }

      public async Task<User> Handle(Query request, CancellationToken cancellationToken)
      {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
          throw new RestException(HttpStatusCode.Unauthorized, new { user = "User not found" });
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
        {
          return new User
          {
            Token = _jwtGenerator.CreateToken(user),
            Username = user.UserName,
            CreatedAt = user.CreatedAt
          };
        }
        else
        {
          throw new RestException(HttpStatusCode.Unauthorized, new { user = "User not found" });
        }

      }
    }
  }
}