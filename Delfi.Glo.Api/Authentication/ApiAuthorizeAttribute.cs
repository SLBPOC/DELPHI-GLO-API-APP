using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Delfi.Glo.Api.Authentication
{
    [AttributeUsage(AttributeTargets.All)]
    public class ApiAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext authorizationFilterContext)
        {
            _ = authorizationFilterContext ?? throw new ArgumentNullException(nameof(authorizationFilterContext));
            bool isAuthenticationEnabled = false;
            if (Environment.GetEnvironmentVariable("IsAuthenticationRequired") != null &&
                   Boolean.Parse(Environment.GetEnvironmentVariable("IsAuthenticationRequired")))
                Boolean.TryParse(Environment.GetEnvironmentVariable("IsAuthenticationRequired"), out isAuthenticationEnabled);

            if (isAuthenticationEnabled)
            {
                if (!authorizationFilterContext.HttpContext.User.Identity!.IsAuthenticated)
                {
                    authorizationFilterContext.Result = new UnauthorizedResult();
                }
            }
        }
    }
}
