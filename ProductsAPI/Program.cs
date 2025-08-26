using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProductsAPI.Authentication;
using ProductsAPI.Authentication.Definitions;
using ProductsAPI.Database;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IHashingService, HashingService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddDbContext<IProductDBContext, ProductDBContext>(options => options.UseSqlServer(builder.Configuration["connectionStrings:DatabaseConnection"]));

ConfigureSwagger(builder);

ConfigureAuthentication(builder);

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy(Policies.UserManagement, p => p.RequireRole(Roles.Admin));
    x.AddPolicy(Policies.GetProducts, p => p.RequireRole(Roles.Admin, Roles.User));
    x.AddPolicy(Policies.EditProducts, p => p.RequireRole(Roles.Admin));
});

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


static void ConfigureSwagger(WebApplicationBuilder builder)
{
    builder.Services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("JwtBearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "JwtBearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
        });
        c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "JwtBearer"
                }
            },
            new string[] {}
        }
    });
    });
}

static void ConfigureAuthentication(WebApplicationBuilder builder)
{
    var key = builder.Configuration["Authentication:Key"];
    if (key is null)
        throw new ArgumentNullException(key);
    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
}