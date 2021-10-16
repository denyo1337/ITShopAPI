using Application;
using Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
namespace WebAPI.Installers
{
    public class MVCInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<Seeder>();
            services.AddInfrastructure();
            services.AddApplication();
            services.AddControllers().AddFluentValidation();
        }
    }
}
