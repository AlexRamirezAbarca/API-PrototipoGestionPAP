namespace WEB_APP_PrototipoGestionPAP.Models
{
    public class BaseResponse<T>
    {
        public string Mensaje { get; set; }
        public T Datos { get; set; }
    }

}
