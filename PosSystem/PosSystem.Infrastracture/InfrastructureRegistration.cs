using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using PosSystem.Core.Interfaces;
using PosSystem.Infrastracture.Persistence;

namespace PosSystem.Infrastracture
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructureRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient(typeof(IRepository<>),typeof(Repository<>));
        }
    }
}
