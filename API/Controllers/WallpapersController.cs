using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities;
using Features.Wallpapers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class WallpapersController : BaseController
  {
    [HttpPost]
    public async Task<ActionResult<WallpaperUploadResult>> Add([FromForm] Add.Command command)
    {
      return await Mediator.Send(command);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Unit>> Delete(string id)
    {
      return await Mediator.Send(new Delete.Command { PublicId = id });
    }
  }
}