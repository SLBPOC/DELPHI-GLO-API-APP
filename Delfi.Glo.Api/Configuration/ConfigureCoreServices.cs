using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Delfi.Glo.Api.Middleware;
using Delfi.Glo.Common.Models;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Common.Services.Interfaces;
namespace Delfi.Glo.Api.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbUnitWork>();
            services.AddTransient<ExceptionMiddleware>();
            services.AddScoped<HttpClient>(s => new HttpClient());
            services.AddScoped(typeof(IHttpService<>), typeof(HttpService<>));
            services.AddSingleton(configuration.GetSection("BaseUrls").Get<BaseUrls>());
            
            services.AddScoped<IAlertService<AlertsDto>, AlertsService>();
            services.AddScoped<ICustomAlertService<CustomAlertDto>, CustomAlertServices>();
            services.AddScoped<IEventService<EventDto>, EventService>();
            services.AddScoped<IWellService<WellDto>, WellService>();
            services.AddScoped<IGeneralInfoService<WellGeneralInfoDto>, WellGeneralInfoService>();
            services.AddScoped<IWellDetailsInfoService<WellDetailsDto>, WellDetailsInfoService>();
            services.AddScoped<ICrudService<CrewDto>, CrewService>();
            services.AddScoped<IUniversityService<UniversitiesDto>, UniversityService>();
            return services;
        }
    }
}
