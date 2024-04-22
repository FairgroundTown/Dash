using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AuthFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Cookies["authToken"];
        // TODO!
        if(token == null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Syd"),
                new Claim(ClaimTypes.Email, "syd@fairgroundtown.co.uk"),
            };
            var identity = new ClaimsIdentity(claims);
            context.HttpContext.User = new ClaimsPrincipal(identity);
            return;
        }
        context.Result = new UnauthorizedResult();
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthAttribute : TypeFilterAttribute
{
    public AuthAttribute() : base(typeof(AuthFilter))
    {
        Arguments = Array.Empty<object>();
    }
}