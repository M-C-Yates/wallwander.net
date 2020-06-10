using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Features.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security
{
  public class JwtGenerator : IJwtGenerator
  {
    private readonly SymmetricSecurityKey _key;
    public JwtGenerator(IConfiguration config)
    {
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
    }

    public string CreateToken(AppUser user)
    {
      var claims = new List<Claim> {
        new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
    };

      // generate signing credentials
      SymmetricSecurityKey key = _key;
      SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
      SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = creds
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
      return tokenHandler.WriteToken(token);
    }
  }
}