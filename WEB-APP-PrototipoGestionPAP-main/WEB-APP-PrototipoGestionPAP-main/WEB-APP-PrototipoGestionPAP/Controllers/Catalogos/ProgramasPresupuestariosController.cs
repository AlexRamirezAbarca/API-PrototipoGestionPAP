using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class ProgramasPresupuestariosController : BaseController<ProgramasPresupuestarios>
    {
        public ProgramasPresupuestariosController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "ProgramasPresupuestarios")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Programas Presupuestarios";
        }

        protected override string GetEntityName()
        {
            return nameof(ProgramasPresupuestarios);
        }

        protected override string GetControllerName()
        {
            return nameof(ProgramasPresupuestariosController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "ProgramaPreId";
        }
    }
}
