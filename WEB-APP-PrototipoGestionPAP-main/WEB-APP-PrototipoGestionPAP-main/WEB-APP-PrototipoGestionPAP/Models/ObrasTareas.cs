using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WEB_APP_PrototipoGestionPAP.Models;

public partial class ObrasTareas
{
    public int ObraTareaId { get; set; }

    public int ActividadId { get; set; }

    public string ObraTarea { get; set; } = null!;

    public DateOnly FechaDesde { get; set; }

    public DateOnly FechaHasta { get; set; }
    public virtual ICollection<EjecucionesMensuales> EjecucionesMensuales { get; set; } = new List<EjecucionesMensuales>();


    public virtual Actividades Actividad { get; set; } = null!;
}
