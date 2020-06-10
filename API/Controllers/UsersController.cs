using System.Threading.Tasks;
using Features.Users;
using Features.Users.Commands;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class UsersController : BaseController
  {
    [HttpPost("Register")]
    public async Task<ActionResult<User>> Register(Register.Command command)
    {
      return await Mediator.Send(command);
    }

  }
}