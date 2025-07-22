using System.Collections.Generic;

namespace WEB_APP_PrototipoGestionPAP.Models
{
    public class Roles
    {
        public int RolId { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public ICollection<Permisos> Permisos { get; set; } = new List<Permisos>();

    }
    public partial class MultiplePermisosRequest
    {
        public int RolId { get; set; }
        public virtual ICollection<Permisos> Permisos { get; set; } = new List<Permisos>();
    }
    public class RolesRequest
    {
        public Roles Rol { get; set; }
        public ICollection<Permisos> Permisos { get; set; } = new List<Permisos>();
    }
}
