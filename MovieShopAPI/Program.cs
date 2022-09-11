using System.Text;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MovieShopAPI.Middlewares;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// dependency Injection
builder.Services.AddScoped<IMovieService, MovieService>(); 
builder.Services.AddScoped<IMovieRepository, MovieRepository>(); 
builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

// inject connection string from appsettings.json into MovieShopDbContext class
builder.Services.AddDbContext<MovieShopDbContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("MovieShopDbConnection")));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
                (builder.Configuration["secretKey"]))

        };
    });
// API is goona use JWT authentication, so that it can look at the incoming request and look for Token and
// if valid it will get the claims into HttpContext
var app = builder.Build();

// Configure the HTTP request pipeline.
// when get a http request from client/browser
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMovieShopExceptionMiddleware();
app.UseHttpsRedirection();

// make sure add Authentication Middleware
// order matters!
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();