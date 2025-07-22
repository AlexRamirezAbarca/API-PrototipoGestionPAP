using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class IndicadoresController : BaseController<Indicadores>
    {
        public IndicadoresController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "Indicadores")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Indicadores";
        }

        protected override string GetEntityName()
        {
            return nameof(Indicadores);
        }

        protected override string GetControllerName()
        {
            return nameof(IndicadoresController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "IndicadoresId";
        }
    }
}
