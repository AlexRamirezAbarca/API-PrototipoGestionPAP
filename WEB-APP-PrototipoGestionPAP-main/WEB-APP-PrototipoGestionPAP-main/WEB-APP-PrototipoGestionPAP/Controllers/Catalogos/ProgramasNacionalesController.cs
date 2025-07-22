using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class ProgramasNacionalesController : BaseController<ProgramasNacionales>
    {
        public ProgramasNacionalesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "ProgramasNacionales")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Programas Nacionales";
        }

        protected override string GetEntityName()
        {
            return nameof(ProgramasNacionales);
        }

        protected override string GetControllerName()
        {
            return nameof(ProgramasNacionalesController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "ProgramaNacId";
        }
    }
}
