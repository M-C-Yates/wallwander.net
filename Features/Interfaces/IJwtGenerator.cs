using Domain.Entities;

namespace Features.Interfaces
{
  public interface IJwtGenerator
  {
    string CreateToken(AppUser user);
  }
}