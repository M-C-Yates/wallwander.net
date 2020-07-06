using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
  public class AppUser : IdentityUser
  {
    public DateTime CreatedAt { get; set; }

    [JsonIgnore]
    public virtual ICollection<Wallpaper> Wallpapers { get; set; }
  }
}