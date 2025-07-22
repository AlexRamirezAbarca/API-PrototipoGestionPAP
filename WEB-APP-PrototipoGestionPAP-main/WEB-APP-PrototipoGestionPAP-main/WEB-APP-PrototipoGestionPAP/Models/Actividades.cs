// Models/Actividades.cs
namespace WEB_APP_PrototipoGestionPAP.Models
{
    public partial class Actividades
    {
        public int ActividadId { get; set; }
        public int? PlanificacionId { get; set; }
        public string? PlanificacionName { get; set; }

        public int? UnidadRespId { get; set; }
        public string? UnidadRespName { get; set; }

        public int? ObjEstrId { get; set; }
        public string? ObjEstrName { get; set; }

        public int? UnidadEjecutoraId { get; set; }
        public string? UnidadEjecutoraName { get; set; }

        public int? ObjetivoOperativoId { get; set; }
        public string? ObjetivoOperativoName { get; set; }

        public string Descripcion { get; set; } = null!;

        public int? CarreraId { get; set; }
        public string? CarreraName { get; set; }

        public int? FacultadId { get; set; }
        public string? FacultadName { get; set; }

        public int? EjePnId { get; set; }
        public string? EjePnName { get; set; }

        public int? ObjPnId { get; set; }
        public string? ObjPnName { get; set; }

        public int? PoliticaPnId { get; set; }
        public string? PoliticaPnName { get; set; }

        public int? MetaPnId { get; set; }
        public string? MetaPnName { get; set; }

        public int? ProgramaNacId { get; set; }
        public string? ProgramaNacName { get; set; }

        public int? ProgramaInstId { get; set; }
        public string? ProgramaInstName { get; set; }

        public int? ProductoInstId { get; set; }
        public string? ProductoInstName { get; set; }

        public string? Impacto { get; set; }
        public string? Riesgo { get; set; }

        public int? AccionId { get; set; }
        public string? AccionName { get; set; }

        public int? IndicadorId { get; set; }
        public string? IndicadorName { get; set; }

        public int? ProgramaPreId { get; set; }
        public string? ProgramaPreName { get; set; }

        public string? ItemPresupuestario { get; set; }

        public decimal Fuente1Monto { get; set; }
        public decimal Fuente2Monto { get; set; }
        public decimal Fuente3Monto { get; set; }

        public decimal RecursosActividad { get; set; }
        public decimal RecursosRestantes { get; set; }
    }
}
