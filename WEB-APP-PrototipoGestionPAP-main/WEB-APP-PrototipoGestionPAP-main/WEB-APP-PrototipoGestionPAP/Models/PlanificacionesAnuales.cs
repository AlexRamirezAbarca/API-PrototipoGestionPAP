using System;
using System.Collections.Generic;

namespace WEB_APP_PrototipoGestionPAP.Models;

public partial class PlanificacionesAnuales
{
    public int PlanificacionId { get; set; }

    public int Anio { get; set; }

    public string? Descripcion { get; set; }

    public string? Estado { get; set; }
}
