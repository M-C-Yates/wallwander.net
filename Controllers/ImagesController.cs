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
  public class ImagesController : ControllerBase
  {

    // GET /images
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
    {
      // return images
    }

    // /images/:id
    [HttpGet("{id}")]
    public ActionResult<string> Get(int id)
    {
      // return image
      return "test";
    }

    [HttpPost]
    public IActionResult Upload(IFormFile image)
    {
      //handle image upload

      return Ok();
    }

    [HttpDelete]
    public void Delete(int id)
    {
      // handle image deletion
    }

  }
}