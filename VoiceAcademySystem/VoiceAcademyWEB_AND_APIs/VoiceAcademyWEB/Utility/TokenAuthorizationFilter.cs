using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;

namespace VoiceAcademyWEB.Utility
{
    public class TokenAuthorizationFilter : IAuthorizationFilter
    {
        private readonly IMemoryCache _cache;

        public TokenAuthorizationFilter(IMemoryCache cache)
        {
            _cache = cache;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!_cache.TryGetValue("Token", out _))
            {
                // Redireccionar al usuario a la página de inicio de sesión
                context.Result = new RedirectToPageResult("/LoginView");
            }
        }
    }
}
