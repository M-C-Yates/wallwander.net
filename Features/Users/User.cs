using System;

namespace Features.Users
{
  public class User
  {
    public string Token { get; set; }
    public string Username { get; set; }
    public DateTime CreatedAt { get; set; }
  }
}