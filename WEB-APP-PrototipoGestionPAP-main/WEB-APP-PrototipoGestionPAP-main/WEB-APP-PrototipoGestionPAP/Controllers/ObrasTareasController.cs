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
    public class ObrasTareasController : Controller
    {
        protected readonly HttpClient _httpClient;
        protected readonly string baseApiUrl;

        // Modified: Remove unused 'apiEndpoint' parameter
        public ObrasTareasController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            baseApiUrl = configuration["ApiSettings:BaseUrl"];
        }
        // GET: ObrasTareas/Index
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10, string filter = null, string filterField = null)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Auth", "Login");
            }

            var permisosJson = HttpContext.Session.GetString("UserPermissions");
            var permisosDict = new Dictionary<string, List<string>>();

            if (!string.IsNullOrEmpty(permisosJson))
            {
                permisosDict = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(permisosJson);
            }

            ViewBag.Permisos = permisosDict;

            var obrasTareasList = new List<ObrasTareas>();
            int totalRecords = 0;
            int totalPages = 0;

            string planificacionId = HttpContext.Session.GetString("PlanificacionId");


            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var url = $"/api/ObrasTareas?page={page}&pageSize={pageSize}&planificacionId={planificacionId}";
                if (!string.IsNullOrEmpty(filter) && !string.IsNullOrEmpty(filterField))
                {
                    url += $"&filter={Uri.EscapeDataString(filter)}&filterField={Uri.EscapeDataString(filterField)}";
                }

                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    var responseData = JsonConvert.DeserializeObject<dynamic>(jsonString);

                    obrasTareasList = JsonConvert.DeserializeObject<List<ObrasTareas>>(responseData.data.ToString());
                    totalRecords = responseData.pagination.totalRecords;
                    totalPages = responseData.pagination.totalPages;
                }
                else
                {
                    TempData["Error"] = "Error al obtener la lista de Obras/Tareas.";
                }
            }

            ViewBag.TotalPages = totalPages;
            ViewBag.CurrentPage = page;
            ViewBag.Filter = filter;
            ViewBag.FilterField = filterField;

            return View(obrasTareasList);
        }

        // POST: ObrasTareas/Agregar
        [HttpPost]
        public async Task<ActionResult> Agregar(ObrasTareas nuevaObraTarea)
        {
            Console.WriteLine("Iniciando método Agregar");
            var token = HttpContext.Session.GetString("AuthToken");
            Console.WriteLine("Token obtenido: " + (string.IsNullOrEmpty(token) ? "Nulo o vacío" : token));

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "No hay token de autenticación.";
                Console.WriteLine("Error: No hay token de autenticación.");
                return RedirectToAction("Index");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                Console.WriteLine("BaseAddress configurado a: " + baseApiUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine("Headers configurados. Enviando petición POST a /api/ObrasTareas");

                var jsonContent = JsonConvert.SerializeObject(nuevaObraTarea);
                Console.WriteLine("Contenido serializado: " + jsonContent);
                var contentString = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PostAsync("/api/ObrasTareas", contentString);
                    Console.WriteLine("Respuesta recibida: " + response.StatusCode);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Se agregó correctamente la Obra/Tarea.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Console.WriteLine("Error al agregar la Obra/Tarea. Código: " + response.StatusCode);
                        TempData["Error"] = "Error al agregar la Obra/Tarea.";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Excepción durante el POST: " + ex.Message);
                    TempData["Error"] = "Se produjo una excepción al agregar la Obra/Tarea.";
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: ObrasTareas/Editar
        [HttpPost]
        public async Task<ActionResult> Editar(ObrasTareas obraTareaEditada)
        {
            Console.WriteLine("Iniciando método Editar");
            var token = HttpContext.Session.GetString("AuthToken");
            Console.WriteLine("Token obtenido: " + (string.IsNullOrEmpty(token) ? "Nulo o vacío" : token));

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "No hay token de autenticación.";
                Console.WriteLine("Error: No hay token de autenticación.");
                return RedirectToAction("Index");
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseApiUrl);
                Console.WriteLine("BaseAddress configurado a: " + baseApiUrl);

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Console.WriteLine("Headers configurados. Enviando petición PUT a /api/ObrasTareas/" + obraTareaEditada.ObraTareaId);

                var jsonContent = JsonConvert.SerializeObject(obraTareaEditada);
                Console.WriteLine("Contenido serializado: " + jsonContent);
                var contentString = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                try
                {
                    var response = await client.PutAsync($"/api/ObrasTareas/{obraTareaEditada.ObraTareaId}", contentString);
                    Console.WriteLine("Respuesta recibida: " + response.StatusCode);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Se editó correctamente la Obra/Tarea.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Console.WriteLine("Error al editar la Obra/Tarea. Código: " + response.StatusCode);
                        TempData["Error"] = "Error al editar la Obra/Tarea.";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Excepción durante el PUT: " + ex.Message);
                    TempData["Error"] = "Se produjo una excepción al editar la Obra/Tarea.";
                    return RedirectToAction("Index");
                }
            }
        }


        // POST: ObrasTareas/Eliminar
        [HttpPost]
        public async Task<ActionResult> Eliminar(int id)
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

                var response = await client.DeleteAsync($"/api/ObrasTareas/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["Error"] = "Error al eliminar la Obra/Tarea.";
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: Agregar una nueva Ejecución Mensual
        [HttpPost]
        public async Task<ActionResult> AgregarEjecucion(EjecucionesMensuales nuevaEjecucion)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            Console.WriteLine("AgregarEjecucion: Token obtenido de sesión.");

            if (string.IsNullOrEmpty(token))
            {
                TempData["Error"] = "No hay token de autenticación.";
                Console.WriteLine("AgregarEjecucion: Token no disponible.");
                return RedirectToAction("Index");
            }

            using (var client = new HttpClient())
            {
                Console.WriteLine("AgregarEjecucion: Configurando cliente HTTP.");
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var jsonContent = JsonConvert.SerializeObject(nuevaEjecucion);
                    Console.WriteLine($"AgregarEjecucion: JSON serializado: {jsonContent}");
                    var contentString = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    Console.WriteLine("AgregarEjecucion: Enviando solicitud POST.");
                    var response = await client.PostAsync("/api/EjecucionesMensuales", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("AgregarEjecucion: Ejecución agregada con éxito.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Console.WriteLine($"AgregarEjecucion: Error en la respuesta. Código: {response.StatusCode}, Mensaje: {await response.Content.ReadAsStringAsync()}");
                        TempData["Error"] = "Error al agregar la ejecución mensual.";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"AgregarEjecucion: Excepción: {ex.Message}");
                    TempData["Error"] = "Error inesperado al agregar la ejecución mensual.";
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: Editar una Ejecución Mensual
        [HttpPost]
        public async Task<ActionResult> EditarEjecucion(EjecucionesMensuales ejecucionEditada)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            Console.WriteLine("EditarEjecucion: Token obtenido de sesión.");
            Console.WriteLine("EditarEjecucion: " + ejecucionEditada.PorcentajeEjecucion);

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("EditarEjecucion: Token no disponible.");
                return RedirectToAction("Index");
            }

            using (var client = new HttpClient())
            {
                Console.WriteLine("EditarEjecucion: Configurando cliente HTTP.");
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    var jsonContent = JsonConvert.SerializeObject(ejecucionEditada);
                    Console.WriteLine($"EditarEjecucion: JSON serializado: {jsonContent}");
                    var contentString = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    Console.WriteLine($"EditarEjecucion: Enviando solicitud PUT para ID: {ejecucionEditada.EjecucionId}.");
                    var response = await client.PutAsync($"/api/EjecucionesMensuales/{ejecucionEditada.EjecucionId}", contentString);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("EditarEjecucion: Ejecución editada con éxito.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Console.WriteLine($"EditarEjecucion: Error en la respuesta. Código: {response.StatusCode}, Mensaje: {await response.Content.ReadAsStringAsync()}");
                        TempData["Error"] = "Error al editar la ejecución mensual.";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"EditarEjecucion: Excepción: {ex.Message}");
                    TempData["Error"] = "Error inesperado al editar la ejecución mensual.";
                    return RedirectToAction("Index");
                }
            }
        }

        // POST: Eliminar una Ejecución Mensual
        [HttpPost]
        public async Task<ActionResult> EliminarEjecucion(int id)
        {
            var token = HttpContext.Session.GetString("AuthToken");
            Console.WriteLine("EliminarEjecucion: Token obtenido de sesión.");

            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("EliminarEjecucion: Token no disponible.");
                return RedirectToAction("Index");
            }

            using (var client = new HttpClient())
            {
                Console.WriteLine("EliminarEjecucion: Configurando cliente HTTP.");
                client.BaseAddress = new Uri(baseApiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                try
                {
                    Console.WriteLine($"EliminarEjecucion: Enviando solicitud DELETE para ID: {id}.");
                    var response = await client.DeleteAsync($"/api/EjecucionesMensuales/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("EliminarEjecucion: Ejecución eliminada con éxito.");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        Console.WriteLine($"EliminarEjecucion: Error en la respuesta. Código: {response.StatusCode}, Mensaje: {await response.Content.ReadAsStringAsync()}");
                        TempData["Error"] = "Error al eliminar la ejecución mensual.";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"EliminarEjecucion: Excepción: {ex.Message}");
                    TempData["Error"] = "Error inesperado al eliminar la ejecución mensual.";
                    return RedirectToAction("Index");
                }
            }
        }

    }
}
