using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDTrabajadores.Models
{
    public class Provincia
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Nombre de la Provincia")]
        public string NombreProvincia { get; set; } = string.Empty;

        [Column("IdDepartamento")]
        public int DepartamentoId { get; set; }
        public Departamento Departamento { get; set; } = null!;
    }
}
