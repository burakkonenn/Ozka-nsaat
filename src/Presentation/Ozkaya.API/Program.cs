using Microsoft.AspNetCore.Authentication.JwtBearer;
using Persistance;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Persistance.Context;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();

builder.Services.AddCors(options =>
     options.AddDefaultPolicy(builder =>
     builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
builder.Services.AddIdentity<User, Role>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.User.RequireUniqueEmail = true;


}).AddEntityFrameworkStores<OzkaDbContext>().AddDefaultTokenProviders();


builder.Services.AddAuthentication(scheme =>
{
    scheme.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    scheme.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    scheme.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["JwtToken:Issuer"],
                    ValidAudience = builder.Configuration["JwtToken:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtToken:Key"]))
                };
            });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("MyPolicy");
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
