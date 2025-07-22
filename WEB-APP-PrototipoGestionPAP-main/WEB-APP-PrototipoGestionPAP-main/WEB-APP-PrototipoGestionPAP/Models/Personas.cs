namespace WEB_APP_PrototipoGestionPAP.Models
{
    public class Personas
    {
        public int PersonaId { get; set; }

        public string Cedula { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string Apellido { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public string? Telefono { get; set; }

        public virtual Usuarios? Usuario { get; set; } = null;
    }
}
