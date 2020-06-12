using System;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
  public class AppUser : IdentityUser
  {
    public int Uploads { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}