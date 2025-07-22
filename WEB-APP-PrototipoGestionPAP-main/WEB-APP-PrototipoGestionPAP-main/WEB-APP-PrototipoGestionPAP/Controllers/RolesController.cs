using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WEB_APP_PrototipoGestionPAP.Models;
using WEB_APP_PrototipoGestionPAP.Models.ViewModels;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class RolesController : BaseController<Roles>
    {
        private readonly IConfiguration _configuration;

        public RolesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "Roles")
        {
            _configuration = configuration;
        }

        protected override string GetCatalogTitle()
        {
            return "Gestión de Roles";
        }

        protected override string GetEntityName()
        {
            return "Roles";
        }

        protected override string GetControllerName()
        {
            return "Roles";
        }

        protected override string GetIdFieldName()
        {
            return "RolId";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Agregar(Roles newItem)
        {
            var permisos = Request.Form["permisos"].ToString();
            var permisosList = ParsePermisos(permisos);

            var rolesRequest = new RolesRequest
            {
                Rol = newItem,
                Permisos = permisosList
            };

            if (!SetAuthorizationHeader())
            {
                TempData["Error"] = "No hay token";
                return RedirectToAction("Index");
            }

            var jsonContent = JsonConvert.SerializeObject(rolesRequest);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_baseApiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Error al agregar el rol.";
            }
            else
            {
                TempData["ToastMessage"] = "Rol agregado correctamente.";
                TempData["ToastType"] = "primary";
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public override async Task<IActionResult> Editar(int id, Roles updatedItem)
        {
            Console.WriteLine("Inicio del método Editar");
            Console.WriteLine($"ID recibido: {id}");
            Console.WriteLine($"Objeto Roles recibido: {JsonConvert.SerializeObject(updatedItem)}");

            var permisos = Request.Form["permisos"].ToString();
            Console.WriteLine($"Permisos recibidos del formulario: {permisos}");

            var permisosList = ParsePermisos(permisos);
            Console.WriteLine($"Lista de permisos parseada: {JsonConvert.SerializeObject(permisosList)}");

            var rolesRequest = new RolesRequest
            {
                Rol = updatedItem,
                Permisos = permisosList
            };
            Console.WriteLine($"Objeto RolesRequest creado: {JsonConvert.SerializeObject(rolesRequest)}");

            if (!SetAuthorizationHeader())
            {
                Console.WriteLine("Fallo al establecer el encabezado de autorización: No hay token.");
                TempData["Error"] = "No hay token";
                return RedirectToAction("Index");
            }
            Console.WriteLine("Encabezado de autorización establecido correctamente.");

            var jsonContent = JsonConvert.SerializeObject(rolesRequest);
            Console.WriteLine($"Contenido JSON serializado: {jsonContent}");

            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            Console.WriteLine("Contenido de la solicitud HTTP PUT creado.");

            Console.WriteLine($"Enviando solicitud PUT a: {_baseApiUrl}/{id}");
            var response = await _httpClient.PutAsync($"{_baseApiUrl}/{id}", content);
            Console.WriteLine($"Respuesta recibida con código de estado: {response.StatusCode}");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("La respuesta indica un fallo en la actualización del rol.");
                var jsonString = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Contenido de la respuesta de error: {jsonString}");

                var responseData = JsonConvert.DeserializeObject<BaseResponse<dynamic>>(jsonString);
                Console.WriteLine($"Mensaje de error deserializado: {responseData?.Mensaje}");

                TempData["Error"] = responseData?.Mensaje ?? "Error al actualizar el rol.";
            }
            else
            {
                Console.WriteLine("Rol actualizado correctamente.");
                TempData["ToastMessage"] = "Rol actualizado correctamente.";
                TempData["ToastType"] = "primary";
            }

            Console.WriteLine("Redirigiendo a la acción Index.");
            return RedirectToAction("Index");
        }


        private List<Permisos> ParsePermisos(string permisos)
        {
            var permisosList = new List<Permisos>();
            if (!string.IsNullOrEmpty(permisos))
            {
                var permisosArray = permisos.Split(',');
                foreach (var permisoStr in permisosArray)
                {
                    permisosList.Add(new Permisos
                    {
                        Codigo = permisoStr,
                        Descripcion = ""
                    });
                }
            }
            return permisosList;
        }
    }
}
