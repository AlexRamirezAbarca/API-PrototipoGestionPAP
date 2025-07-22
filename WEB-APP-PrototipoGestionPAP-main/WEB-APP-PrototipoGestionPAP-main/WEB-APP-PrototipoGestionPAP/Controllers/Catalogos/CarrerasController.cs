using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class CarrerasController : BaseController<Carreras>
    {
        public CarrerasController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "Carreras")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Carreras";
        }

        protected override string GetEntityName()
        {
            return nameof(Carreras);
        }

        protected override string GetControllerName()
        {
            return nameof(CarrerasController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "CarreraId";
        }
    }
}
