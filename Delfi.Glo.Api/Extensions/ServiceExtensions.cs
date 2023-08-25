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
            services.AddScoped<DbUnitWork>();

            services.AddScoped<ICrudService<CrewDto>, CrewService>();
            services.AddScoped<ICrudService<WellGeneralInfoDto>, WellGeneralInfoService>();

            //builder.Services.AddScoped<IWellService<GeneralInfoDto>, WellService>();
            services.AddScoped<ICrudService<EventDto>, EventService>();
            services.AddScoped<ICrudService<WellDto>, WellService>();
            services.AddScoped<ICrudService<CustomAlertDto>, CustomAlertServices>();
        }
    }
}
