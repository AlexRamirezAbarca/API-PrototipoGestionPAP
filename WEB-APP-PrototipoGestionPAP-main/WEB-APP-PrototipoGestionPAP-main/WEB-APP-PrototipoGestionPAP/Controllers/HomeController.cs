using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using WEB_APP_PrototipoGestionPAP.Models;

namespace WEB_APP_PrototipoGestionPAP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                // Redirigir al login si no hay token
                return RedirectToAction("Login", "Auth");
            }

            var planificacionId = HttpContext.Session.GetString("PlanificacionId");
            if (string.IsNullOrEmpty(planificacionId))
            {
                // Indicar en caso de que quieras usarlo en la vista (no imprescindible con la lógica del Layout)
                ViewBag.ShowPlanificacionModal = true;
            }

            var userName = HttpContext.Session.GetString("UserName") ?? "Usuario";
            ViewBag.UserName = userName;
            ViewBag.PlanificacionId = planificacionId;

            // Mostrar la página principal
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetPlanificacion(int planificacionId)
        {
            HttpContext.Session.SetString("PlanificacionId", planificacionId.ToString());
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
