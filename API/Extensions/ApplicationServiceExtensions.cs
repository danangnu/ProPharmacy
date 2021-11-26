using API.Data;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVersionRepository, VersionRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IDocRepository, DocRepository>();
            services.AddScoped<ISchedulePaymentRepository, SchedulePaymentRepository>();
            services.AddScoped<IPrescriptionSummaryRepository, PrescriptionSummaryRepository>();
            services.AddScoped<ISalesSummaryRepository, SalesSummaryRepository>();
            services.AddScoped<IExpenseSummaryRepository, ExpenseSummaryRepository>();
            services.AddScoped<IUserReportRepository, UserReportRepository>();
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}