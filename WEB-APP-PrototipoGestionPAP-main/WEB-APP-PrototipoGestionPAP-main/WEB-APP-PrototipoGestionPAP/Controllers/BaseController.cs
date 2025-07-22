// Controllers/BaseController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WEB_APP_PrototipoGestionPAP.Models;
using WEB_APP_PrototipoGestionPAP.Models.ViewModels;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public abstract class BaseController<TModel> : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly string _baseApiUrl;

        protected BaseController(IHttpClientFactory httpClientFactory, IConfiguration configuration, string apiEndpoint)
        {
            _httpClient = httpClientFactory.CreateClient();
            _baseApiUrl = $"{configuration["ApiSettings:BaseUrl"]}/api/{apiEndpoint}";
        }

        protected bool SetAuthorizationHeader()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }

        protected abstract string GetCatalogTitle();
        protected abstract string GetEntityName();
        protected abstract string GetControllerName();
        protected abstract string GetIdFieldName();

        public virtual async Task<IActionResult> Index(int page = 1, int pageSize = 10, string filter = null, string filterField = null)
        {
            if (!SetAuthorizationHeader())
            {
                return RedirectToAction("Auth", "Login");
            }

            var permisosJson = HttpContext.Session.GetString("UserPermissions");
            var permisosDict = new Dictionary<string, List<string>>();

            if (!string.IsNullOrEmpty(permisosJson))
            {
                permisosDict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(permisosJson);
            }

            var url = $"{_baseApiUrl}?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(filter) && !string.IsNullOrWhiteSpace(filterField))
            {
                url += $"&filter={Uri.EscapeDataString(filter)}&filterField={Uri.EscapeDataString(filterField)}";
            }

            var itemList = new List<TModel>();
            int totalRecords = 0, totalPages = 0;

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var responseData = JsonConvert.DeserializeObject<BaseResponse<dynamic>>(jsonString);
                itemList = JsonConvert.DeserializeObject<List<TModel>>(Convert.ToString(responseData.Datos.data));
                totalRecords = responseData.Datos.pagination.totalRecords;
                totalPages = responseData.Datos.pagination.totalPages;
            }
            else
            {
                TempData["Error"] = "Error al obtener la lista.";
            }

            var viewModel = new CatalogViewModel<TModel>
            {
                Title = GetCatalogTitle(),
                EntityName = GetEntityName(),
                ControllerName = GetControllerName(),
                CurrentFilter = filter,
                CurrentFilterField = filterField,
                CurrentPage = page,
                TotalPages = totalPages,
                Items = itemList,
                Permissions = permisosDict
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Agregar(TModel newItem)
        {
            Console.WriteLine("Inicio del método Agregar.");

            if (newItem == null)
            {
                Console.WriteLine("El parámetro newItem es null.");
                TempData["Error"] = "El elemento proporcionado es inválido.";
                return RedirectToAction("Index");
            }

            Console.WriteLine("Serializando el nuevo elemento.");
            var jsonContent = JsonConvert.SerializeObject(newItem);
            Console.WriteLine($"Contenido JSON serializado: {jsonContent}");

            var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            Console.WriteLine("Contenido preparado para la solicitud HTTP POST.");

            Console.WriteLine("Estableciendo el encabezado de autorización.");
            if (!SetAuthorizationHeader())
            {
                Console.WriteLine("Fallo al establecer el encabezado de autorización: No hay token.");
                TempData["Error"] = "No hay token";
                return RedirectToAction("Index");
            }
            Console.WriteLine("Encabezado de autorización establecido correctamente.");

            Console.WriteLine($"Enviando solicitud HTTP POST a {_baseApiUrl}.");
            HttpResponseMessage response = null;
            try
            {
                response = await _httpClient.PostAsync(_baseApiUrl, content);
                Console.WriteLine($"Respuesta recibida con el estado: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción al enviar la solicitud POST: {ex.Message}");
                TempData["Error"] = "Ocurrió un error al comunicar con el servidor.";
                return RedirectToAction("Index");
            }

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error al agregar el elemento. Código de estado: {response.StatusCode}");
                TempData["Error"] = "Error al agregar el elemento.";
            }
            else
            {
                Console.WriteLine("Elemento agregado correctamente.");
                TempData["ToastMessage"] = "Elemento agregado correctamente.";
                TempData["ToastType"] = "primary";
            }

            Console.WriteLine("Redirigiendo a la acción Index.");
            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Editar(int id, TModel updatedItem)
        {
            Console.WriteLine($"[INFO] Iniciando el proceso de edición para el ID: {id}");

            // Verificar y establecer el encabezado de autorización
            if (!SetAuthorizationHeader())
            {
                Console.WriteLine("[WARN] No se pudo establecer el encabezado de autorización. Redirigiendo a Index.");
                return RedirectToAction("Index");
            }
            Console.WriteLine("[INFO] Encabezado de autorización establecido correctamente.");

            try
            {
                // Serializar el objeto actualizado a JSON
                var jsonContent = JsonConvert.SerializeObject(updatedItem);
                Console.WriteLine($"[DEBUG] Objeto serializado: {jsonContent}");

                var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
                Console.WriteLine("[INFO] Contenido preparado para la solicitud PUT.");

                // Realizar la solicitud PUT al API
                Console.WriteLine($"[INFO] Enviando solicitud PUT a: {_baseApiUrl}/{id}");
                var response = await _httpClient.PutAsync($"{_baseApiUrl}/{id}", content);
                Console.WriteLine($"[DEBUG] Código de respuesta recibido: {(int)response.StatusCode} ({response.ReasonPhrase})");

                if (!response.IsSuccessStatusCode)
                {
                    // Leer el contenido de la respuesta para obtener más detalles del error
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"[ERROR] Error al actualizar el elemento. Detalles: {errorContent}");

                    TempData["Error"] = "Error al actualizar el elemento.";
                }
                else
                {
                    Console.WriteLine("[INFO] Elemento actualizado correctamente.");
                    TempData["ToastMessage"] = "Elemento actualizado correctamente.";
                    TempData["ToastType"] = "primary";
                }
            }
            catch (Exception ex)
            {
                // Capturar y registrar cualquier excepción que ocurra durante el proceso
                Console.WriteLine($"[EXCEPTION] Ocurrió una excepción durante la actualización: {ex.Message}");
                TempData["Error"] = "Ocurrió un error inesperado al actualizar el elemento.";
            }

            Console.WriteLine("Redirigiendo a la acción Index.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<IActionResult> Eliminar(int id)
        {
            if (!SetAuthorizationHeader())
            {
                return RedirectToAction("Index");
            }

            var response = await _httpClient.DeleteAsync($"{_baseApiUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Error al eliminar el elemento.";
            }
            else
            {
                TempData["ToastMessage"] = "Elemento eliminado correctamente.";
                TempData["ToastType"] = "primary";
            }
            return RedirectToAction("Index");
        }
    }
}
