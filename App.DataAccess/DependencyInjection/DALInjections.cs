using App.DataAccess.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.DataAccess.DependencyInjection
{
    public static class DALInjections
    {
        public static void InstallDALDependencyInjections(this IServiceCollection services)
        {
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
