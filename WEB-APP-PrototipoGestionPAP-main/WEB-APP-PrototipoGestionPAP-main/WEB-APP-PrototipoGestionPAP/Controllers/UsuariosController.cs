using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class UsuariosController : BaseController<Usuarios>
    {
        public UsuariosController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "Usuarios")
        {
        }

        protected override string GetCatalogTitle() => "Gestionar Usuarios";
        protected override string GetEntityName() => "Usuarios";
        protected override string GetControllerName() => "Usuarios";
        protected override string GetIdFieldName() => "UsuarioId";

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Agregar(Usuarios newItem)
        {
            if (!SetAuthorizationHeader())
            {
                TempData["Error"] = "No hay token";
                return RedirectToAction("Index");
            }

            if (newItem.Persona != null && !string.IsNullOrEmpty(newItem.Persona.CorreoElectronico))
            {
                var personaJson = JsonConvert.SerializeObject(newItem.Persona);
                var personaContent = new StringContent(personaJson, Encoding.UTF8, "application/json");
                var personaUrl = $"{_baseApiUrl.Replace("/api/Usuarios", "/api/Personas")}";
                var personaResponse = await _httpClient.PostAsync(personaUrl, personaContent);

                if (personaResponse.IsSuccessStatusCode)
                {
                    var responseContent = await personaResponse.Content.ReadAsStringAsync();
                    var personaResponseObject = JsonConvert.DeserializeObject<BaseResponse<Personas>>(responseContent);
                    newItem.PersonaId = personaResponseObject.Datos.PersonaId;
                }
                else
                {
                    TempData["Error"] = "Error al agregar la persona.";
                    return RedirectToAction("Index");
                }
            }

            return await base.Agregar(newItem);
        }
    }
}
