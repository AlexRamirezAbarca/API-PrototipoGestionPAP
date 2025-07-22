using System;
using System.Collections.Generic;

namespace WEB_APP_PrototipoGestionPAP.Models;

public partial class Indicadores
{
    public int IndicadorId { get; set; }

    public string NombreIndicador { get; set; } = null!;

    public string MetodoCalculo { get; set; } = null!;

    public string MetaIndicador { get; set; } = null!;
}
