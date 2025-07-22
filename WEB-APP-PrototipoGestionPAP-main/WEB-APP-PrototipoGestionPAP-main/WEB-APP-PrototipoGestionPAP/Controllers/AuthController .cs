using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class AuthController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly string baseApiUrl;

        // Modified: Remove unused 'apiEndpoint' parameter
        public AuthController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            baseApiUrl = configuration["ApiSettings:BaseUrl"];
        }


        [HttpGet]
        public ActionResult Login()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (!string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Roles()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Denegado()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Message = "Por favor, completa todos los campos.";
                ViewBag.MessageType = "error";
                return View(model);
            }

            var requestBody = new
            {
                nombreUsuario = model.NombreUsuario,
                contraseña = model.Contraseña
            };

            var json = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{baseApiUrl}/api/Auth/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<BaseResponse<TokenResponse>>(responseBody);

                // Guardar datos en sesión
                HttpContext.Session.SetString("UserName", model.NombreUsuario);
                HttpContext.Session.SetString("AuthToken", tokenResponse.Datos.Token);

                var permisosJson = JsonConvert.SerializeObject(tokenResponse.Datos.Permisos);
                HttpContext.Session.SetString("UserPermissions", permisosJson);

                // Redirigir a la página principal (Home) donde se mostrará el modal para seleccionar Planificación
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
            return View(model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("AuthToken");
            HttpContext.Session.Remove("UserName");
            HttpContext.Session.Remove("UserPermissions");
            HttpContext.Session.Remove("PlanificacionId");
            return RedirectToAction("Login", "Auth");
        }
    }

    public class TokenResponse
    {
        public string Token { get; set; }
        public Dictionary<string, List<string>> Permisos { get; set; }
    }
}
