using System;
using System.Collections.Generic;

namespace WEB_APP_PrototipoGestionPAP.Models;

public partial class ProgramasPresupuestarios
{
    public int ProgramaPreId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }
}
