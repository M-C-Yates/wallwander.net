using System;
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
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Features.Users.Commands
{
  public class Register
  {
    public class Command : IRequest<User>
    {
      public string Username { get; set; }
      public string Email { get; set; }
      public string Password { get; set; }
    }

    public class CommandValidator : AbstractValidator<Command>
    {
      public CommandValidator()
      {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3).MaximumLength(25);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).Password();
      }
    }
    public class Handler : IRequestHandler<Command, User>
    {
      private readonly DataContext _context;
      private readonly UserManager<AppUser> _userManager;
      private readonly IJwtGenerator _jwtGenerator;

      public Handler(DataContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
      {
        this._context = context;
        this._userManager = userManager;
        this._jwtGenerator = jwtGenerator;
      }

      public async Task<User> Handle(Command request, CancellationToken cancellationToken)
      {
        if (await _context.Users.AnyAsync(x => x.Email == request.Email))
        {
          throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });
        }
        if (await _context.Users.AnyAsync(x => x.UserName == request.Username))
        {
          throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });
        }

        var user = new AppUser
        {
          UserName = request.Username,
          Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
          return new User
          {
            Token = _jwtGenerator.CreateToken(user),
            Username = user.UserName,
            CreatedAt = DateTime.Now
          };
        }
        else
        {
          throw new RestException(HttpStatusCode.InternalServerError, new { user = "problem creating user" });
        }
      }
    }
  }
}