using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class ObjetivosOperativosController : BaseController<ObjetivosOperativos>
    {
        public ObjetivosOperativosController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "ObjetivosOperativos")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Objetivos Operativos";
        }

        protected override string GetEntityName()
        {
            return nameof(ObjetivosOperativos);
        }

        protected override string GetControllerName()
        {
            return nameof(ObjetivosOperativosController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "ObjetivoOperativoId";
        }
    }
}
