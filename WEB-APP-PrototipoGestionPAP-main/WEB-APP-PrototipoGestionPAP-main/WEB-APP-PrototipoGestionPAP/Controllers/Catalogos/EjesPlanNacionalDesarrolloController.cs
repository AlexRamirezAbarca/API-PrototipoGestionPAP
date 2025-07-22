using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class EjesPlanNacionalDesarrolloController : BaseController<EjesPlanNacionalDesarrollo>
    {
        public EjesPlanNacionalDesarrolloController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "EjesPlanNacionalDesarrollo")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Ejes del Plan Nacional de Desarrollo";
        }

        protected override string GetEntityName()
        {
            return nameof(EjesPlanNacionalDesarrollo);
        }

        protected override string GetControllerName()
        {
            return nameof(EjesPlanNacionalDesarrolloController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "EjePnId";
        }
    }
}
