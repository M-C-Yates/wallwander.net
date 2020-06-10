using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MediatR;
using Persistence;
using Domain;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Features.Users.Commands;
using FluentValidation.AspNetCore;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using AutoMapper;
using Features.Interfaces;
using Infrastructure.Security;
using API.Middleware;

namespace API
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<DataContext>(opt =>
      {
        opt.UseLazyLoadingProxies();
        opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
      });

      services.AddCors(opt =>
      {
        opt.AddPolicy("CorsPolicy", policy =>
        {
          policy.AllowAnyHeader().AllowAnyMethod()/*.WithOrigins("https://wallwander.net) */;
        });
      });

      services.AddMediatR(typeof(Register.Handler).Assembly);
      services.AddAutoMapper(typeof(Register.Handler));

      services.AddControllers(opt =>
      {

      }).AddFluentValidation(cfg =>
      {
        cfg.RegisterValidatorsFromAssemblyContaining<Register>();
      });

      services.AddDefaultIdentity<AppUser>()
      .AddEntityFrameworkStores<DataContext>()
      .AddSignInManager<SignInManager<AppUser>>();

      services.AddScoped<IJwtGenerator, JwtGenerator>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      app.UseMiddleware<ErrorHandlingMiddleware>();
      // if (env.IsDevelopment())
      // {
      //   app.UseDeveloperExceptionPage();
      // }

      // app.UseHttpsRedirection();

      app.UseRouting();
      app.UseCors("CorsPolicy");

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
