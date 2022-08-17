using Application.Authentication;
using Application.Features.Vehicles.Commands.CreateVehicle;
using Application.Interfaces;
using Application.Middleware;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddTransient<ErrorHandlingMiddleware>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<IValidator<RegisterRentalCompanyRequest>, RegisterRentalCompanyValidator>();
            services.AddScoped<IValidator<RegisterCustomerRequest>, RegisterCustomerValidator>();
            



            return services;
        }
    }
}
