// Controllers/PersonasController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using WEB_APP_PrototipoGestionPAP.Models;
using WEB_APP_PrototipoGestionPAP.Models.ViewModels;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class PersonasController : BaseController<Personas>
    {
        public PersonasController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "Personas")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Gestión de Personas";
        }

        protected override string GetEntityName()
        {
            return "Personas";
        }

        protected override string GetControllerName()
        {
            return nameof(Personas).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "PersonaId";
        }

    }
}
