using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PosSystem.Contracts.Client;
using PosSystem.Core.Interfaces;
using PosSystem.Services.Helpers;
using System.Text;
namespace PosSystem.Services
{
    public static class ServicesRegistration
    {
        public static void AddServicesRegistration(this WebApplicationBuilder builder)
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
            builder.Services.AddAutoMapper(typeof(ServicesRegistration).Assembly);

            // Add HttpContextAccessor for getting the current user
            builder.Services.AddHttpContextAccessor();

            #region Services Registeration
            builder.Services.AddScoped(typeof(IUserServices<,>), typeof(UserServices<,>));
            builder.Services.AddScoped<IAuthServices, AuthServices>();
            builder.Services.AddScoped(typeof(ICompanyServices<,>), typeof(CompanyServices<,>));
            builder.Services.AddScoped(typeof(IClientService<,>), typeof(ClientServices<,>));


            #endregion

            #region Helpers Registeration
            builder.Services.AddTransient<TokenGeneratorHelper>();
            #endregion
        }
    }
}