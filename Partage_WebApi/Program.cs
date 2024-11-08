using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Partage.Data;
using Partage.Models;
using Partage.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PartageDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PartageDbContext") ?? throw new InvalidOperationException("Connection string 'PartageDbContext' not found."));
    options.UseLazyLoadingProxies();


});
// Add services to the container.
builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<PartageDbContext>();

builder.Services.Configure<GeocodingOptions>(builder.Configuration.GetSection("Geocoding"));
builder.Services.AddScoped<GeocodingService>();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{

    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{

    options.SaveToken = true;
    options.RequireHttpsMetadata = false; // Développement
    options.TokenValidationParameters = new TokenValidationParameters()

    {

        ValidateAudience = true,
        ValidateIssuer = true,
        ValidAudience = "http://localhost:4200",
        ValidIssuer = "http://localhost:5026",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Cette fonction est couverte par la loi du Quebec"))

    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Allow all", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Allow all");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
