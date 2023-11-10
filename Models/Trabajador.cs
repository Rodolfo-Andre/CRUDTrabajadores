using CRUDTrabajadores.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUDTrabajadores.Models
{
    public class Trabajador
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required(ErrorMessage = "El tipo de documento es requerido")]
        public string TipoDocumento { get; set; } = string.Empty;

        [Display(Name = "Numero de Documento")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "Se permite mínimo 8 caracteres máximo 12")]
        [Required(ErrorMessage = "El número de documento es requerido")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El número de documento debe ser un valor numérico")]
        public string NumeroDocumento { get; set; } = string.Empty;

        [Display(Name = "Nombres y Apellidos")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Se permite mínimo 3 caracteres máximo 50")]
        [Required(ErrorMessage = "El nombre completo es requerido")]
        public string Nombres { get; set; } = string.Empty;

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "El sexo es requerido")]
        public string Sexo { get; set; } = string.Empty;

        [Display(Name = "Teléfono")]
        [StringLength(16, MinimumLength = 9, ErrorMessage = "Se permite mínimo 9 caracteres máximo 16")]
        [Required(ErrorMessage = "El teléfono es requerido")]
        [RegularExpression(@"^(?:\+\d{2}-\d+|\d+)$", ErrorMessage = "Debe ser ún número telefónico, se admite también el código del país, ejemplo: +51-924022741 o 924022741")]
        public string Telefono { get; set; } = string.Empty;

        [Column("IdDepartamento")]
        [Display(Name = "Departamento")]
        [Required(ErrorMessage = "El departamento es requerido")]
        public int DepartamentoId { get; set; }
        public Departamento? Departamento { get; set; }

        [Column("IdProvincia")]
        [Display(Name = "Provincia")]
        [Required(ErrorMessage = "La provincia es requerida")]
        public int ProvinciaId { get; set; }
        public Provincia? Provincia { get; set; } 

        [Column("IdDistrito")]
        [Display(Name = "Distrito")]
        [Required(ErrorMessage = "El distrito es requerido")]
        public int DistritoId { get; set; }
        public Distrito? Distrito { get; set; }
    }
}
