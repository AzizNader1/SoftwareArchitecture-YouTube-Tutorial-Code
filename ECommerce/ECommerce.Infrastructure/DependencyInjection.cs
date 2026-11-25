using ECommerce.Application.Interfaces;
using ECommerce.Application.Interfaces.ReadInterfaces;
using ECommerce.Application.Interfaces.WriteInterfaces;
using ECommerce.Infrastructure.Persistence.Contexts;
using ECommerce.Infrastructure.Repositories;
using ECommerce.Infrastructure.Repositories.ReadRepos;
using ECommerce.Infrastructure.Repositories.WriteRepos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Server=(localdb)\\mssqllocaldb;Database=ECommerceDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

            // 1. Register Segregated DbContexts
            services.AddDbContext<ApplicationReadDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddDbContext<ApplicationWriteDbContext>(options =>
                options.UseSqlServer(connectionString));

            // 2. Register Generic Repositories
            services.AddScoped(typeof(IGenericReadRepository<>), typeof(GenericReadRepository<>));
            services.AddScoped(typeof(IGenericWriteRepository<>), typeof(GenericWriteRepository<>));

            // 3. Register Entity Read Repositories
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();

            // 4. Register Entity Write Repositories
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            // 5. Register Unit of Work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
