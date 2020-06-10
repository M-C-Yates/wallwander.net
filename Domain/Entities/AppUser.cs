using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
  public class AppUser : IdentityUser
  {
    public int Uploads { get; set; }
  }
}