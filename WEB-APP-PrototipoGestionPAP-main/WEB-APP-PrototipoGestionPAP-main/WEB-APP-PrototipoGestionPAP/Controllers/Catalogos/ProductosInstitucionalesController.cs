using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class ProductosInstitucionalesController : BaseController<ProductosInstitucionales>
    {
        public ProductosInstitucionalesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "ProductosInstitucionales")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Productos Institucionales";
        }

        protected override string GetEntityName()
        {
            return nameof(ProductosInstitucionales);
        }

        protected override string GetControllerName()
        {
            return nameof(ProductosInstitucionalesController).Replace("Controller", "");
        }

        protected override string GetIdFieldName()
        {
            return "ProductoInstId";
        }
    }
}
