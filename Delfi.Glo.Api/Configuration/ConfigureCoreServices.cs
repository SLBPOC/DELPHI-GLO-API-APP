using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
using Delfi.Glo.Api.Middleware;
using Delfi.Glo.Common.Models;
using Delfi.Glo.Common.Services;
using Delfi.Glo.Common.Services.Interfaces;
using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.Repository;
namespace Delfi.Glo.Api.Configuration
{
    public static class ConfigureCoreServices
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DbUnitWork>();
            services.AddScoped<ICrudService<CrewDto>, CrewService>();
            services.AddScoped<ICrudService<WellGeneralInfoDto>, WellGeneralInfoService>();
            services.AddScoped<IAlertService<AlertsDto>, AlertsService>();
            //builder.Services.AddScoped<IWellService<GeneralInfoDto>, WellService>();
            services.AddScoped<IEventService<EventDto>, EventService>();
            services.AddScoped<ICrudService<WellDto>, WellService>();
            services.AddScoped<ICustomAlertService<CustomAlertDto>, CustomAlertServices>();
            services.AddScoped<IUniversityService<UniversitiesDto>, UniversityService>();
            services.AddScoped<IWellService<WellDetailsDto>, WellService>();
            services.AddScoped(typeof(IHttpService<>), typeof(HttpService<>));
            services.AddTransient<ExceptionMiddleware>();
            services.AddScoped<HttpClient>(s => new HttpClient());
            services.AddSingleton(configuration.GetSection("BaseUrls").Get<BaseUrls>());
            return services;
        }
    }
}
