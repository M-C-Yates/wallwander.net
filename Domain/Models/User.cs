using System.Collections.Generic;
using Microsoft.AspNetCore;

namespace Wallwander.Domain.Models
{
  public class User
  {
    public string id;
    public string Email;
    public string userName;
    public string password;
    public string firstName;
    public string lastName;
    public IList<Image> uploads { get; set; } = new List<Image>();
  }
}