// Controllers/ObjetivosEstrategicosInstitucionalesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class ObjetivosEstrategicosInstitucionalesController : BaseController<ObjetivosEstrategicosInstitucionales>
    {
        public ObjetivosEstrategicosInstitucionalesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "ObjetivosEstrategicosInstitucionales")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Objetivos Estratégicos Institucionales";
        }

        protected override string GetEntityName()
        {
            return nameof(ObjetivosEstrategicosInstitucionales);
        }

        protected override string GetControllerName()
        {
            return nameof(ObjetivosEstrategicosInstitucionalesController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "ObjEstrId";
        }
    }
}
