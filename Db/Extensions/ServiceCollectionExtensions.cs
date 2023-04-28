using Db.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Db.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterDbServices(this IServiceCollection services)
        {
            services.AddTransient<ICameraRepository, CameraRepository>();
            return services;
        }
    }
}
