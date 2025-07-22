// Controllers/ActividadesController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WEB_APP_PrototipoGestionPAP.Models;
using WEB_APP_PrototipoGestionPAP.Models.ViewModels;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class ActividadesController : BaseController<Actividades>
    {
        public ActividadesController(IHttpClientFactory httpClientFactory, IConfiguration configuration)
            : base(httpClientFactory, configuration, "Actividades")
        {
        }

        protected override string GetCatalogTitle()
        {
            return "Catálogo de Actividades";
        }

        protected override string GetEntityName()
        {
            return "Actividades";
        }

        protected override string GetControllerName()
        {
            return "Actividades";
        }

        protected override string GetIdFieldName()
        {
            return "ActividadId";
        }

        // Overriding Index to handle 'planificacionId'
        public override async Task<IActionResult> Index(int page = 1, int pageSize = 10, string filter = null, string filterField = null)
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

            // Safely retrieve planificacionId from query
            string planificacionId = HttpContext.Session.GetString("PlanificacionId");
            planificacionId = planificacionId ?? ""; // avoid null

            // Build URL with planificacionId
            string url = $"{_baseApiUrl}?page={page}&pageSize={pageSize}";
            if (!string.IsNullOrWhiteSpace(planificacionId))
            {
                url += $"&planificacionId={Uri.EscapeDataString(planificacionId)}";
            }
            if (!string.IsNullOrWhiteSpace(filter) && !string.IsNullOrWhiteSpace(filterField))
            {
                url += $"&filter={Uri.EscapeDataString(filter)}&filterField={Uri.EscapeDataString(filterField)}";
            }

            var actividadList = new List<Actividades>();
            int totalRecords = 0;
            int totalPages = 0;

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<BaseResponse<dynamic>>(jsonString);

                if (responseData?.Datos != null)
                {
                    actividadList = JsonConvert.DeserializeObject<List<Actividades>>(Convert.ToString(responseData.Datos.data));
                    totalRecords = responseData.Datos.pagination.totalRecords;
                    totalPages = responseData.Datos.pagination.totalPages;
                }
            }
            else
            {
                TempData["Error"] = "Error retrieving activities.";
            }

            var viewModel = new CatalogViewModel<Actividades>
            {
                Title = GetCatalogTitle(),
                EntityName = GetEntityName(),
                ControllerName = GetControllerName(),
                CurrentFilter = filter,
                CurrentFilterField = filterField,
                CurrentPage = page,
                TotalPages = totalPages,
                Items = actividadList,
                Permissions = permisosDict
            };

            return View(viewModel);
        }
    }
}
