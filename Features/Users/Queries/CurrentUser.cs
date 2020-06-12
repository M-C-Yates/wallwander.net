using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Features.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Features.Users.Queries
{
  public class CurrentUser
  {
    public class Query : IRequest<User> { }

    public class Handler : IRequestHandler<Query, User>
    {
      private readonly UserManager<AppUser> _userManager;
      private readonly IJwtGenerator _jwtGenerator;
      private readonly IUserAccessor _userAccessor;

      public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator, IUserAccessor userAccessor)
      {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
        _userAccessor = userAccessor;
      }

      public async Task<User> Handle(Query request, CancellationToken cancellationToken)
      {
        // handler logic
        var user = await _userManager.FindByNameAsync(_userAccessor.GetCurrentUsername());

        return new User
        {
          Username = user.UserName,
          Token = _jwtGenerator.CreateToken(user),
          CreatedAt = user.CreatedAt
        };
      }
    }
  }
}