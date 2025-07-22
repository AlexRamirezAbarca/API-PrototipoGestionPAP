using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class ObjetivosPlanNacionalDesarrolloController : BaseController<ObjetivosPlanNacionalDesarrollo>
    {
        public ObjetivosPlanNacionalDesarrolloController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "ObjetivosPlanNacionalDesarrollo")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Objetivos del Plan Nacional de Desarrollo";
        }

        protected override string GetEntityName()
        {
            return nameof(ObjetivosPlanNacionalDesarrollo);
        }

        protected override string GetControllerName()
        {
            return nameof(ObjetivosPlanNacionalDesarrolloController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "ObjPnId";
        }
    }
}
