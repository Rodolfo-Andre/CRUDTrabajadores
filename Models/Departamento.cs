using System.ComponentModel.DataAnnotations;

namespace CRUDTrabajadores.Models
{
    public class Departamento
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Nombre del Departamento")]
        public string NombreDepartamento { get; set; } = string.Empty;
    }
}
