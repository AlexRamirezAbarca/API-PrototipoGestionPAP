using System.Collections.Generic;

namespace WEB_APP_PrototipoGestionPAP.Models.ViewModels
{
    public class CatalogViewModel<T>
    {
        public string Title { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentFilterField { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; } = 1;
        public List<T> Items { get; set; }
        public Dictionary<string, List<string>> Permissions { get; set; }

        public bool CanWrite => Permissions != null &&
                                 Permissions.ContainsKey(EntityName) &&
                                 Permissions[EntityName].Contains("Escritura");

        public bool CanUpdate => Permissions != null &&
                                  Permissions.ContainsKey(EntityName) &&
                                  Permissions[EntityName].Contains("Actualización");

        public bool CanDelete => Permissions != null &&
                                  Permissions.ContainsKey(EntityName) &&
                                  Permissions[EntityName].Contains("Eliminación");

        public string ControllerName { get; set; }
        public string AddActionName { get; set; } = "Agregar";
        public string EditActionName { get; set; } = "Editar";
        public string EntityName { get; set; }
    }
}
