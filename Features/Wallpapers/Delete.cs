using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Features.Errors;
using Features.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Features.Wallpapers
{
  public class Delete
  {
    public class Command : IRequest
    {
      public string PublicId { get; set; }
    }

    public class Handler : IRequestHandler<Command>
    {
      private readonly DataContext _context;
      private readonly IUserAccessor _userAccessor;
      private readonly IWallpaperAccessor _wallpaperAccessor;

      public Handler(DataContext context, IUserAccessor userAccessor, IWallpaperAccessor wallpaperAccessor)
      {
        _wallpaperAccessor = wallpaperAccessor;
        _userAccessor = userAccessor;
        _context = context;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
      {
        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUsername());

        var wallpaper = user.Wallpapers.FirstOrDefault(x => x.PublicId == request.PublicId);

        if (wallpaper == null)
        {
          throw new RestException(HttpStatusCode.NotFound, new
          {
            Wallpaper = "not found"
          });
        }

        var result = _wallpaperAccessor.DeleteWallpaper(wallpaper.PublicId);

        if (result == null)
        {
          throw new Exception("Problem deleting wallpaper");
        }

        _context.Wallpapers.Remove(wallpaper);

        var success = await _context.SaveChangesAsync() > 0;
        if (success)
        {
          return Unit.Value;
        }
        else
        {
          throw new Exception("Problem saving changes");
        }
      }
    }
  }
}