using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WEB_APP_PrototipoGestionPAP.Models;
using Microsoft.AspNetCore.Http;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class EjecucionesMensualesController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly string baseApiUrl;

        protected EjecucionesMensualesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            baseApiUrl = $"{configuration["ApiSettings:BaseUrl"]}/api/EjecucionesMensuales";
        }
        // GET: EjecucionesMensuales
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10, string filter = null, string filterField = null)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Auth", "Login");
            }

            var ejecucionesList = new List<EjecucionesMensuales>();
            int totalRecords = 0;
            int totalPages = 0;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Construir la URL con los parámetros de consulta
                var url = $"/api/EjecucionesMensuales?page={page}&pageSize={pageSize}";
                if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
                {
                    url += $"&filter={Uri.EscapeDataString(filter)}&filterField={Uri.EscapeDataString(filterField)}";
                }

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonString);

                    ejecucionesList = JsonConvert.DeserializeObject<List<EjecucionesMensuales>>(responseData.data.ToString());
                    totalRecords = responseData.pagination.totalRecords;
                    totalPages = responseData.pagination.totalPages;
                }
                else
                {
                    TempData["Error"] = "Error al obtener la lista de ejecuciones mensuales.";
                }
            }

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.Filter = filter;
            ViewBag.FilterField = filterField;
            return View(ejecucionesList);
        }

        // POST: Agregar una nueva Ejecución Mensual
        [HttpPost]
        public async Task<ActionResult> Agregar(EjecucionesMensuales nuevaEjecucion)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "No hay token de autenticación.";
                return RedirectToAction("Index");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(nuevaEjecucion);
                var contentString = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync("/api/EjecucionesMensuales", contentString);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Error al agregar la ejecución mensual.";
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: Editar una Ejecución Mensual
        [HttpPost]
        public async Task<ActionResult> Editar(EjecucionesMensuales ejecucionEditada)
        {
            // Logs para ver si realmente está llegando un porcentaje extraño
            Console.WriteLine("=== [EditarEjecucion] Inicio ===");
            Console.WriteLine("EjecucionId: " + ejecucionEditada.EjecucionId);
            Console.WriteLine("ObraTareaId: " + ejecucionEditada.ObraTareaId);
            Console.WriteLine("Mes: " + ejecucionEditada.Mes);
            Console.WriteLine("Monto: " + ejecucionEditada.Monto);
            Console.WriteLine("PorcentajeEjecucion: " + ejecucionEditada.PorcentajeEjecucion);
            Console.WriteLine("Descripcion: " + (ejecucionEditada.Descripcion ?? "(null)"));

            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Token vacío, redirigiendo a Index...");
                return RedirectToAction("Index");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var jsonContent = JsonConvert.SerializeObject(ejecucionEditada);
                Console.WriteLine("JSON a enviar => " + jsonContent);

                var contentString = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                // Se asume que la API identifica el recurso por EjecucionId
                var apiUrl = $"/api/EjecucionesMensuales/{ejecucionEditada.EjecucionId}";
                Console.WriteLine("PUT => " + apiUrl);

                var response = await client.PutAsync(apiUrl, contentString);

                Console.WriteLine("Respuesta del API: " + response.StatusCode);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("=== [EditarEjecucion] PUT Exitoso ===");
                    return RedirectToAction("Index");
                }
                else
                {
                    Console.WriteLine("=== [EditarEjecucion] Error => " + await response.Content.ReadAsStringAsync());
                    TempData["Error"] = "Error al editar la ejecución mensual.";
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: Eliminar una Ejecución Mensual
        [HttpPost]
        public async Task<ActionResult> Eliminar(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.DeleteAsync($"/api/EjecucionesMensuales/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Error al eliminar la ejecución mensual.";
                    return RedirectToAction("Index");
                }
            }
        }
    }
}
