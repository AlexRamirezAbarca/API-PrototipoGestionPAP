using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

public class CatalogosController : Controller
{
    public IActionResult Index()
    {
        string permisosJson = HttpContext.Session.GetString("UserPermissions");
        Dictionary<string, List<string>> permisos = permisosJson != null
            ? JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(permisosJson)
            : new Dictionary<string, List<string>>();

        var todosLosCatalogos = new List<Catalogo>
        {
            new Catalogo { Nombre = "Ejes Plan Nacional Desarrollo", ImagenUrl = "/images/banner.png", Enlace = "/EjesPlanNacionalDesarrollo" },
            new Catalogo { Nombre = "Objetivos Plan Nacional Desarrollo", ImagenUrl = "/images/banner.png", Enlace = "/ObjetivosPlanNacionalDesarrollo" },
            new Catalogo { Nombre = "Políticas Plan Nacional Desarrollo", ImagenUrl = "/images/banner.png", Enlace = "/PoliticasPlanNacionalDesarrollo" },
            new Catalogo { Nombre = "Metas Plan Nacional Desarrollo", ImagenUrl = "/images/banner.png", Enlace = "/MetasPlanNacionalDesarrollo" },
            new Catalogo { Nombre = "Programas Nacionales", ImagenUrl = "/images/banner.png", Enlace = "/ProgramasNacionales" },
            new Catalogo { Nombre = "Programas Institucionales", ImagenUrl = "/images/banner.png", Enlace = "/ProgramasInstitucionales" },
            new Catalogo { Nombre = "Productos Institucionales", ImagenUrl = "/images/banner.png", Enlace = "/ProductosInstitucionales" },
            new Catalogo { Nombre = "Unidades Responsables", ImagenUrl = "/images/banner.png", Enlace = "/UnidadesResponsables" },
            new Catalogo { Nombre = "Objetivos Estratégicos", ImagenUrl = "/images/banner.png", Enlace = "/ObjetivosEstrategicosInstitucionales" },
            new Catalogo { Nombre = "Unidades Ejecutoras", ImagenUrl = "/images/banner.png", Enlace = "/UnidadesEjecutoras" },
            new Catalogo { Nombre = "Objetivos Operativos", ImagenUrl = "/images/banner.png", Enlace = "/ObjetivosOperativos" },
            new Catalogo { Nombre = "Acciones", ImagenUrl = "/images/banner.png", Enlace = "/Acciones" },
            new Catalogo { Nombre = "Carreras", ImagenUrl = "/images/banner.png", Enlace = "/Carreras" },
            new Catalogo { Nombre = "Facultades", ImagenUrl = "/images/banner.png", Enlace = "/Facultades" },
            new Catalogo { Nombre = "Indicadores", ImagenUrl = "/images/banner.png", Enlace = "/Indicadores" },
            new Catalogo { Nombre = "Programas Presupuestarios", ImagenUrl = "/images/banner.png", Enlace = "/ProgramasPresupuestarios" }
        };

        var catalogosPermitidos = todosLosCatalogos
            .Where(c => permisos.ContainsKey(GetPermisoKey(c.Nombre)) && permisos[GetPermisoKey(c.Nombre)].Contains("Lectura"))
            .ToList();

        return View(catalogosPermitidos);
    }

    private string GetPermisoKey(string nombreCatalogo)
    {
        return nombreCatalogo switch
        {
            "Ejes Plan Nacional Desarrollo" => "EjesPlanNacionalDesarrollo",
            "Objetivos Plan Nacional Desarrollo" => "ObjetivosPlanNacionalDesarrollo",
            "Políticas Plan Nacional Desarrollo" => "PoliticasPlanNacionalDesarrollo",
            "Metas Plan Nacional Desarrollo" => "MetasPlanNacionalDesarrollo",
            "Programas Nacionales" => "ProgramasNacionales",
            "Programas Institucionales" => "ProgramasInstitucionales",
            "Productos Institucionales" => "ProductosInstitucionales",
            "Unidades Responsables" => "UnidadesResponsables",
            "Objetivos Estratégicos" => "ObjetivosEstrategicosInstitucionales",
            "Unidades Ejecutoras" => "UnidadesEjecutoras",
            "Objetivos Operativos" => "ObjetivosOperativos",
            "Acciones" => "Acciones",
            "Carreras" => "Carreras",
            "Facultades" => "Facultades",
            "Indicadores" => "Indicadores",
            "Programas Presupuestarios" => "ProgramasPresupuestarios",
            _ => ""
        };
    }
}

public class Catalogo
{
    public string Nombre { get; set; }
    public string ImagenUrl { get; set; }
    public string Enlace { get; set; }
}
