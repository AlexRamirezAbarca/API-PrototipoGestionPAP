using Newtonsoft.Json;

public class PermisoAuthorizationMiddleware
{
    private readonly RequestDelegate _next;

    public PermisoAuthorizationMiddleware(RequestDelegate next) => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        // Obtener ruta actual en minúsculas
        var path = context.Request.Path.Value?.ToLowerInvariant() ?? "";
        var controller = (context.GetRouteData().Values["controller"]?.ToString() ?? "").ToLowerInvariant();

        // 1. Validar rutas públicas
        var publicRoutes = new[] { "/", "/auth/login", "/home/index", "/catalogos" };
        if (publicRoutes.Contains(path) || controller == "auth" || controller == "home")
        {
            await _next(context);
            return;
        }

        // 3. Validación granular de permisos
        var permisos = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(context.Session.GetString("UserPermissions") ?? "{}");
        var tienePermiso = ValidarPermisoPorRuta(controller, permisos);

        if (!tienePermiso)
        {
            context.Response.Redirect("/Auth/Denegado");
            return;
        }

        await _next(context);
    }

    // Lógica de validación mejorada
    private bool ValidarPermisoPorRuta(string controller, Dictionary<string, List<string>> permisos)
    {
        // Convertir todo a minúsculas para validación consistente
        var controlador = controller.ToLowerInvariant();
        permisos = permisos.ToDictionary(
            k => k.Key.ToLowerInvariant(),
            v => v.Value.Select(p => p.ToLowerInvariant()).ToList()
        );

        // Verificar acceso específico al controlador
        return permisos.TryGetValue(controlador, out var acciones) &&
               acciones.Contains("lectura");
    }


}

public static class StringExtensions
{
    public static bool Contains(this IEnumerable<string> source, string value, StringComparison comparisonType)
        => source?.Any(s => s.Equals(value, comparisonType)) ?? false;
}