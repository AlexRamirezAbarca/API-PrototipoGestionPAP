using System.ComponentModel.DataAnnotations;

namespace WEB_APP_PrototipoGestionPAP.Models
{
    public class Permisos
    {
        public int PermisoId { get; set; }

        public string Codigo { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
