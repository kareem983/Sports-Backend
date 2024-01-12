using AutoMapper;
using Core.Abstractions;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DI
{
    public static class SpoertsDI
    {
        public static void InfastructureStrapping(this IServiceCollection Services)
        {
            Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped<IAccountService, AccountService>();
            Services.AddScoped<IAccountService, AccountService>();

        }
    }
}
