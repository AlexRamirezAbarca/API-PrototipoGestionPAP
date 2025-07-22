using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    /// <summary>
    /// Controlador del Front para generar y visualizar Reportes,
    /// consumiendo la API publicada en Azure.
    /// </summary>
    public class ReportesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly string _apiBaseUrl;

        public ReportesController(IConfiguration configuration)
        {
            _configuration = configuration;

            // O bien leer desde appsettings.json, sección "ApiSettings:BaseUrl"
           _apiBaseUrl = _configuration["ApiSettings:BaseUrl"] ?? "https://localhost:5001";
        }

        /// <summary>
        /// Muestra la vista "Index" que contiene
        /// el formulario para generar y visualizar reportes.
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // =========================================================================
        // MÉTODOS PARA GENERAR Y RETORNAR PDF DESDE LA API
        // =========================================================================

        /// <summary>
        /// Genera el PDF de "Todas las Planificaciones" (con o sin filtrado por año).
        /// Llama a: GET /api/Reportes/TodasPlanificacionesPdf?anio=
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GenerarTodasPlanificaciones(int? anio)
        {
            // Armar endpoint de la API
            string endpoint = anio.HasValue
                ? $"{_apiBaseUrl}/api/Reportes/TodasPlanificacionesPdf?anio={anio}"
                : $"{_apiBaseUrl}/api/Reportes/TodasPlanificacionesPdf";

            // Consumir la API
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Error al generar el reporte: Todas las Planificaciones.");

            // Retornar el PDF al navegador
            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", "Reporte_Todas_Planificaciones.pdf");
        }

        /// <summary>
        /// Genera el PDF de "Detalle de una Planificación".
        /// Llama a: GET /api/Reportes/DetallePlanificacionPdf?planificacionId=
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GenerarDetallePlanificacion(int planificacionId)
        {
            string endpoint = $"{_apiBaseUrl}/api/Reportes/DetallePlanificacionPdf?planificacionId={planificacionId}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Error al generar el reporte: Detalle de Planificación.");

            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", $"Detalle_Planificacion_{planificacionId}.pdf");
        }

        /// <summary>
        /// Genera el PDF de "Actividades por Planificación".
        /// Llama a: GET /api/Reportes/ActividadesPorPlanificacionPdf?planificacionId=
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GenerarActividadesPorPlanificacion(int planificacionId)
        {
            string endpoint = $"{_apiBaseUrl}/api/Reportes/ActividadesPorPlanificacionPdf?planificacionId={planificacionId}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Error al generar el reporte: Actividades por Planificación.");

            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", $"Actividades_Planificacion_{planificacionId}.pdf");
        }

        /// <summary>
        /// Genera el PDF de "Ejecución Presupuestaria (Plan vs. Real)".
        /// Llama a: GET /api/Reportes/PlanVsRealPdf?planificacionId=
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GenerarPlanVsReal(int planificacionId)
        {
            string endpoint = $"{_apiBaseUrl}/api/Reportes/PlanVsRealPdf?planificacionId={planificacionId}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Error al generar el reporte: Plan vs. Real.");

            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", $"PlanVsReal_{planificacionId}.pdf");
        }

        /// <summary>
        /// Genera el PDF de "Obras/Tareas y Cronograma".
        /// Llama a: GET /api/Reportes/ObrasTareasCronogramaPdf?planificacionId=
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GenerarObrasTareasCronograma(int planificacionId)
        {
            string endpoint = $"{_apiBaseUrl}/api/Reportes/ObrasTareasCronogramaPdf?planificacionId={planificacionId}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Error al generar el reporte: Obras/Tareas y Cronograma.");

            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", $"ObrasTareas_{planificacionId}.pdf");
        }

        /// <summary>
        /// Genera el PDF de "Avance Mensual Consolidado".
        /// Llama a: GET /api/Reportes/AvanceMensualConsolidadoPdf?planificacionId=
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GenerarAvanceMensualConsolidado(int planificacionId)
        {
            string endpoint = $"{_apiBaseUrl}/api/Reportes/AvanceMensualConsolidadoPdf?planificacionId={planificacionId}";

            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(endpoint);

            if (!response.IsSuccessStatusCode)
                return BadRequest("Error al generar el reporte: Avance Mensual Consolidado.");

            var pdfBytes = await response.Content.ReadAsByteArrayAsync();
            return File(pdfBytes, "application/pdf", $"AvanceMensual_{planificacionId}.pdf");
        }
    }
}
