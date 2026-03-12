using Microsoft.AspNetCore.Authorization;
using NetCoreSeguridadEmpleados.Repositories;
using System.Security.Claims;

namespace NetCoreSeguridadEmpleados.Policies
{
    public class TieneSubordinadosRequirement : IAuthorizationRequirement { }
    public class TieneSubordinadosHandler : AuthorizationHandler<TieneSubordinadosRequirement>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TieneSubordinadosHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TieneSubordinadosRequirement requirement)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null) return;

            //int id = int.Parse(context.User.FindFirstValue("NameIdentifier"));
            int id = int.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));

            RepositoryHospital repo = httpContext.RequestServices.GetService<RepositoryHospital>();
            if (await repo.TieneSubordinados(id))
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
    //public class TieneSubordinadosRequirement : AuthorizationHandler<TieneSubordinadosRequirement>, IAuthorizationRequirement
    //{
    //    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TieneSubordinadosRequirement requirement)
    //    {
    //        var httpContext = context.Resource as HttpContext;


    //        //int id = int.Parse(context.User.FindFirstValue("NameIdentifier"));
    //        int id = int.Parse(context.User.FindFirstValue(ClaimTypes.NameIdentifier));

    //        RepositoryHospital repo = httpContext.RequestServices.GetService<RepositoryHospital>();
    //        if (await repo.TieneSubordinados(id))
    //        {
    //            context.Succeed(requirement);
    //        }
    //        else
    //        {
    //            context.Fail();
    //        }
    //    }
    //}
}
