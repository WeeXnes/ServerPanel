namespace ServerPanel.Core;


public class AuthService
{

    public static string AdminPassword = Globals.AdminPassword;
    public static string AuthCookieName = "AuthToken";
    public static string ValidToken = Globals.ValidToken;
    public bool IsAuthenticated(HttpContext httpContext)
    {
        if (httpContext.Request.Cookies.TryGetValue(AuthCookieName, out string token))
        {
            return token == ValidToken;
        }
        return false;
    }
}

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, AuthService authService)
    {
        if (context.Request.Path.StartsWithSegments("/dashboard") && !authService.IsAuthenticated(context))
        {
            context.Response.Redirect("/login");
            return;
        }

        await _next(context);
    }
}