using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class UnidadesEjecutorasController : BaseController<UnidadesEjecutoras>
    {
        public UnidadesEjecutorasController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "UnidadesEjecutoras")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Unidades Ejecutoras";
        }

        protected override string GetEntityName()
        {
            return nameof(UnidadesEjecutoras);
        }

        protected override string GetControllerName()
        {
            return nameof(UnidadesEjecutorasController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "UnidadEjecutoraId";
        }
    }
}
