using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WEB_APP_PrototipoGestionPAP.Models;

public partial class RolPermisos
{
    public int RolPermisoId { get; set; }

    public int RolId { get; set; }

    public int PermisoId { get; set; }
    [JsonIgnore]
    public string? Estado { get; set; }

    public int? CreadoPor { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public int? ModificadoPor { get; set; }

    public DateTime? FechaModificacion { get; set; }
    public virtual Permisos Permiso { get; set; } = null!;
    [JsonIgnore]
    public virtual Roles Rol { get; set; } = null!;
}
