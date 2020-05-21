using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace Wallwander.Controllers
{
  [ApiController]
  [Route("/[controller]")]
  public class UsersController : ControllerBase
  {

    // GET /users
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
      // return users
    }

    // /users/:id
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
      // return user

      return "test";
    }

    [HttpPost]
    public void Post([FromBody] string userName)
    {
      // handle registration
    }

    [HttpDelete]
    public void Delete(int id)
    {
      // handle user deletion
    }

  }
}