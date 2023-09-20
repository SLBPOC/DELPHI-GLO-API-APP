using Delfi.Glo.Entities.Dto;
using Delfi.Glo.PostgreSql.Dal.Services;
using Delfi.Glo.PostgreSql.Dal;
using Delfi.Glo.Repository;
using System.Security.Principal;
using Microsoft.Extensions.DependencyInjection;

namespace Delfi.Glo.Api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IAlertService<AlertsDto>, AlertsService>();
            services.AddScoped<IEventService<EventDto>, EventService>();
            
            services.AddScoped<IWellService<WellDto>, WellService>();

            services.AddScoped<IGeneralInfoService<WellGeneralInfoDto>, WellGeneralInfoService>();
            services.AddScoped<IWellDetailsInfoService<WellDetailsDto>, WellDetailsInfoService>();
            //services.AddScoped<IWellDetailsInfoService<SwimLaneGraphDetails>, WellService>();
            services.AddScoped<ICustomAlertService<CustomAlertDto>, CustomAlertServices>();
            services.AddScoped<ICrudService<CrewDto>, CrewService>();
            services.AddScoped<IUniversityService<UniversitiesDto>, UniversityService>();


        }
    }
}
