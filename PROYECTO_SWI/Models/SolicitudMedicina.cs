using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROYECTO_SWI.Models
{
    [Table("Solicitud_Medicina")]
    public class SolicitudMedicina
    {
        [Key]
        [Column("id_solicitud")]
        public int IdSolicitud { get; set; }

        [Required]
        [ForeignKey("Medicina")]
        [Column("id_medicina")]
        public int IdMedicina { get; set; }
        public Medicina? Medicina { get; set; }

        [Required]
        [Column("cantidad_solicitada")]
        public int CantidadSolicitada { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
    }
}
