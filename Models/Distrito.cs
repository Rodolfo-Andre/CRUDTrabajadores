using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDTrabajadores.Models
{
    public class Distrito
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Nombre del Distrito")]
        public string NombreDistrito { get; set; } = string.Empty;

        [Column("IdProvincia")]
        public int ProvinciaId { get; set; }
        public Provincia Provincia { get; set; } = null!;
    }
}
