using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Features.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Features.Wallpapers
{
  public class Add
  {
    public class Command : IRequest<WallpaperUploadResult>
    {
      public IFormFile File { get; set; }
    }

    public class Handler : IRequestHandler<Command, WallpaperUploadResult>
    {
      private readonly DataContext _context;
      private readonly IUserAccessor _userAccessor;
      private readonly IWallpaperAccessor _wallpaperAccessor;

      public Handler(DataContext context, IUserAccessor userAccessor, IWallpaperAccessor wallpaperAccessor)
      {
        _context = context;
        _userAccessor = userAccessor;
        _wallpaperAccessor = wallpaperAccessor;
      }

      public async Task<WallpaperUploadResult> Handle(Command request, CancellationToken cancellationToken)
      {
        var wallpaperUploadResult = _wallpaperAccessor.AddWallpaper(request.File);

        var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == _userAccessor.GetCurrentUsername());

        var wallpaper = new Wallpaper
        {
          Url = wallpaperUploadResult.Url,
          PublicId = wallpaperUploadResult.PublicId,
          Views = 0,
        };

        user.Wallpapers.Add(wallpaper);

        var success = await _context.SaveChangesAsync() > 0;

        if (success)
        {
          return wallpaperUploadResult;
        }
        else
        {
          throw new Exception("Problem saving changes");
        }
      }
    }
  }
}