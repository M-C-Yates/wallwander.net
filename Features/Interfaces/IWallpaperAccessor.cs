using Features.Wallpapers;
using Microsoft.AspNetCore.Http;

namespace Features.Interfaces
{
  public interface IWallpaperAccessor
  {
    WallpaperUploadResult AddWallpaper(IFormFile file);
    string DeleteWallpaper(string publicId);
  }
}