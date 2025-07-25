using System.Text;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Verbum.API.Data;
using Verbum.API.Interfaces.Repositories;
using Verbum.API.Interfaces.Services;
using Verbum.API.Repositories;
using Verbum.API.Services;

namespace Verbum.API;

public class Program {
    public static void Main(string[] args) {
        Env.Load();
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
        builder.Services.AddScoped<ICommunityService, CommunityService>();
        builder.Services.AddTransient<TokenService>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => {
            options.Events = new JwtBearerEvents {
                OnMessageReceived = context => {
                    context.Token = context.Request.Cookies["token"];
                    return Task.CompletedTask;
                }
            };
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.ASCII.GetBytes(Configuration.JWT_SECRET)
                )
            };
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}