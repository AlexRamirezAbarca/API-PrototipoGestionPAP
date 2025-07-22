using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class FacultadesController : BaseController<Facultades>
    {
        public FacultadesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "Facultades")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Facultades";
        }

        protected override string GetEntityName()
        {
            return nameof(Facultades);
        }

        protected override string GetControllerName()
        {
            return nameof(FacultadesController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "FacultadId";
        }
    }
}
