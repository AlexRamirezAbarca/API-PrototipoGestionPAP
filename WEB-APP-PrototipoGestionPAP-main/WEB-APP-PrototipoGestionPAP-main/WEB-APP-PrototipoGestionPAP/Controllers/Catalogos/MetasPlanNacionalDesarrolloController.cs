using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class MetasPlanNacionalDesarrolloController : BaseController<MetasPlanNacionalDesarrollo>
    {
        public MetasPlanNacionalDesarrolloController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "MetasPlanNacionalDesarrollo")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Metas del Plan Nacional de Desarrollo";
        }

        protected override string GetEntityName()
        {
            return nameof(MetasPlanNacionalDesarrollo);
        }

        protected override string GetControllerName()
        {
            return nameof(MetasPlanNacionalDesarrolloController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "MetaPnId";
        }
    }
}
