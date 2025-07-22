namespace WEB_APP_PrototipoGestionPAP.Models
{
    public class Usuarios
    {

        public int UsuarioId { get; set; }

        public int? PersonaId { get; set; }

        public int RolId { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Contraseña { get; set; } = null!;

        public virtual Personas? Persona { get; set; }

        public virtual Roles? Rol { get; set; } = null!;
    }
}
