using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTO_SWI.Models
{
    [Table("Medicinas")]
    public class Medicina
    {
        [Key]
        [Column("id_medicina")]
        public int IdMedicina { get; set; }

        [Required]
        [Column("nombre_medicina")]
        public string? NombreMedicina { get; set; }

        [Required]
        [Column("stock")]
        public int Stock { get; set; }

        [Required]
        [Column("descripcion")]
        public string? Descripcion { get; set; }

        public ICollection<SolicitudMedicina>? Solicitudes { get; set; }
    }
}
