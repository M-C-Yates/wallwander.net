using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Features.Interfaces;
using Features.Wallpapers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Infrastructure.Wallpapers
{
  public class WallpaperAccessor : IWallpaperAccessor
  {
    private readonly Cloudinary _cloudinary;

    public WallpaperAccessor(IOptions<CloudinarySettings> config)
    {
      var acc = new Account(
          config.Value.CloudName,
          config.Value.ApiKey,
          config.Value.ApiSecret
      );

      _cloudinary = new Cloudinary(acc);
    }

    public WallpaperUploadResult AddWallpaper(IFormFile file)
    {
      var uploadResult = new ImageUploadResult();

      if (file.Length > 0)
      {
        using (var stream = file.OpenReadStream())
        {
          var uploadParams = new ImageUploadParams
          {
            File = new FileDescription(file.FileName, stream)
          };
          uploadResult = _cloudinary.Upload(uploadParams);
        }
      }

      return new WallpaperUploadResult
      {
        PublicId = uploadResult.PublicId,
        Url = uploadResult.SecureUrl.AbsoluteUri
      };
    }

    public string DeleteWallpaper(string publicId)
    {
      throw new System.NotImplementedException();
    }
  }
}