using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROYECTO_SWI.Models
{
    [Table("Atenciones")]
    public class Atencion
    {
        [Key]
        [Column("id_atencion")]
        public int IdAtencion { get; set; }

        [Required]
        [ForeignKey("Paciente")]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }
        public Paciente? Paciente { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("fecha")]
        public DateTime Fecha { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [Column("hora")]
        public TimeSpan Hora { get; set; }

        [Required]
        [Column("detalles")]
        public string? Detalles { get; set; }

        public ICollection<Reporte>? Reportes { get; set; }
    }
}
