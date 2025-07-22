using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class UnidadesResponsablesController : BaseController<UnidadesResponsables>
    {
        public UnidadesResponsablesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "UnidadesResponsables")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Unidades Responsables";
        }

        protected override string GetEntityName()
        {
            return nameof(UnidadesResponsables);
        }

        protected override string GetControllerName()
        {
            return nameof(UnidadesResponsablesController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "UnidadRespId";
        }
    }
}
