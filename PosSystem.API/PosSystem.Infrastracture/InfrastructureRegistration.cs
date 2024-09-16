using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PosSystem.Core.Interfaces;
using PosSystem.Core.Interfaces.Repositories;
using PosSystem.Infrastracture.Persistence;
using PosSystem.Infrastracture.Persistence.Data;

namespace PosSystem.Infrastracture
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructureRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<PosDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.UseLazyLoadingProxies();
            });


            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}