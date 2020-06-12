using System.Threading.Tasks;
using Features.Users;
using Features.Users.Commands;
using Features.Users.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
  public class UsersController : BaseController
  {

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<ActionResult<User>> Register(Register.Command command)
    {
      return await Mediator.Send(command);
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult<User>> Login(Login.Query query)
    {
      return await Mediator.Send(query);
    }

    [HttpGet]
    public async Task<ActionResult<User>> CurrentUser()
    {
      return await Mediator.Send(new CurrentUser.Query());
    }

  }
}