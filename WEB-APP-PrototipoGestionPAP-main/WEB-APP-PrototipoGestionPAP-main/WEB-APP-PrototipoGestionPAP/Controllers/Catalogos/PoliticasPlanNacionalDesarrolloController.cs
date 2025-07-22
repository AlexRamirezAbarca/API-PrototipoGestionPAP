using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class PoliticasPlanNacionalDesarrolloController : BaseController<PoliticasPlanNacionalDesarrollo>
    {
        public PoliticasPlanNacionalDesarrolloController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "PoliticasPlanNacionalDesarrollo")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Políticas del Plan Nacional de Desarrollo";
        }

        protected override string GetEntityName()
        {
            return nameof(PoliticasPlanNacionalDesarrollo);
        }

        protected override string GetControllerName()
        {
            return nameof(PoliticasPlanNacionalDesarrolloController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "PoliticaPnId";
        }
    }
}
