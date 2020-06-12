using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
  public class AppUser : IdentityUser
  {
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<Image> Uploads { get; set; }
  }
}