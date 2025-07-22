using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class AccionesController : BaseController<Acciones>
    {
        public AccionesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "Acciones")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Acciones";
        }

        protected override string GetEntityName()
        {
            return nameof(Acciones);
        }

        protected override string GetControllerName()
        {
            return nameof(AccionesController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "AccionId";
        }
    }
}
