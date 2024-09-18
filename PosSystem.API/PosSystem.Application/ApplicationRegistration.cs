using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PosSystem.Application.Interfaces.IServices;
using PosSystem.Application.Services;
using PosSystem.Application.Services.Helpers;
using System.Text;
namespace PosSystem.Application
{
    public static class ApplicationRegistration
    {
        public static void AddApplicationRegistration(this WebApplicationBuilder builder)
        {
            // Register JWT Authentication
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(ApplicationRegistration).Assembly);

            // Add HttpContextAccessor for getting the current user
            builder.Services.AddHttpContextAccessor();

            #region Services Registeration
            builder.Services.AddScoped<IUserServices, UserServices>();
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped<IClientServices, ClientServices>();
            builder.Services.AddScoped<ICompanyServices, CompanyServices>();
            builder.Services.AddScoped<ITypeServices, TypeServices>();
            builder.Services.AddScoped<IUnitServices, UnitServices>();
            builder.Services.AddScoped<IProductServices, ProductServices>();


            #endregion

            #region Helpers Registeration
            builder.Services.AddTransient<TokenGeneratorHelper>();
            #endregion
        }
    }
}