using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace TodoApiDTO.DataAccess
{
    public static class DataContextHelper
    {
        public static void AddSqlServerDataContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TodoContext>(options =>
                options.UseSqlServer(connectionString));

        }
    }
}
