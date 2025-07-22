using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class ProgramasInstitucionalesController : BaseController<ProgramasInstitucionales>
    {
        public ProgramasInstitucionalesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "ProgramasInstitucionales")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Programas Institucionales";
        }

        protected override string GetEntityName()
        {
            return nameof(ProgramasInstitucionales);
        }

        protected override string GetControllerName()
        {
            return nameof(ProgramasInstitucionalesController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "ProgramaInstId";
        }
    }
}
