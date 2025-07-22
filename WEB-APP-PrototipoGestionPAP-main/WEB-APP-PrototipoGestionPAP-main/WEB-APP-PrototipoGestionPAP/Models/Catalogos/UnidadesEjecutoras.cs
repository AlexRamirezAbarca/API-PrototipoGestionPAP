using System;
using System.Collections.Generic;

namespace WEB_APP_PrototipoGestionPAP.Models;

public partial class UnidadesEjecutoras
{
    public int UnidadEjecutoraId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
