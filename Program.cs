global using VehicleRental.API.Dtos;
global using VehicleRental.API.Models;
global using VehicleRental.API.Services.UserService;
global using VehicleRental.API.Services.VehicleService;
global using VehicleRental.API.Services.ReservationService;
global using VehicleRental.API.Services.AuthService;
global using VehicleRental.API.Data;
global using VehicleRental.API.Exceptions;
global using VehicleRental.API.Helpers;
global using Microsoft.EntityFrameworkCore;
global using AutoMapper;
global using System.Text.Json.Serialization;
global using System.ComponentModel.DataAnnotations;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.Authorization;
global using NodaTime;
global using System.ComponentModel.DataAnnotations.Schema;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => {
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IVehicleService, VehicleService>();
builder.Services.AddTransient<IReservationService, ReservationService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RoleClaimType = ClaimTypes.Role,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("JWT:ValidIssuer").Value!,
        ValidAudience = builder.Configuration.GetSection("JWT:ValidAudience").Value!,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:SecretKey").Value!))
    };
}).Services.AddAuthorization(options => 
{
    options.AddPolicy("Admin", policy => policy.RequireRole(Role.ADMIN.ToString()));
    options.AddPolicy("User", policy => policy.RequireRole(Role.USER.ToString()));
    options.AddPolicy("AdminOrUser", policy => policy.RequireRole(Role.USER.ToString()));
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
