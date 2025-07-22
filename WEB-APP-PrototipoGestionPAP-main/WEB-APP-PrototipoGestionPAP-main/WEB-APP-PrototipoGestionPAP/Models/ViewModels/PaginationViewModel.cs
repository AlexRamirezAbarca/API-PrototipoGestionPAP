namespace WEB_APP_PrototipoGestionPAP.Models.ViewModels
{
    public class PaginationViewModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Filter { get; set; }
        public string FilterField { get; set; }
    }
}
