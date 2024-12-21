using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROYECTO_SWI.Models
{
    [Table("Reportes")]
    public class Reporte
    {
        [Key]
        [Column("id_reporte")]
        public int IdReporte { get; set; }

        [Required]
        [ForeignKey("Atencion")]
        [Column("id_atencion")]
        public int IdAtencion { get; set; }
        public Atencion? Atencion { get; set; }

        [Column("detalles_adicionales")]
        public string? DetallesAdicionales { get; set; }
    }
}
