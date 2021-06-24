using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApiDTO.Application.Interfaces.Repositories;
using TodoApiDTO.Application.Interfaces.Services;
using TodoApiDTO.Infrastructure.DBContexts;
using TodoApiDTO.Infrastructure.Repositories;
using TodoApiDTO.Infrastructure.Services;

namespace TodoApiDTO.Infrastructure
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddTransient<ITodoItemRepository, TodoItemRepository>();
            services.AddTransient<ITodoItemService, TodoItemService>();
            return services;
        }
    }
}
