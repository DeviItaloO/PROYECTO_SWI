using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PROYECTO_SWI.Models
{
    [Table("Historial_Pacientes")]
    public class HistorialPaciente
    {
        [Key]
        [Column("id_historial")]
        public int IdHistorial { get; set; }

        [Required]
        [Column("dni_paciente")]
        public string? DniPaciente { get; set; }

        //public Paciente? Paciente { get; set; }

        [Required]
        [Column("numero_atenciones")]
        public int NumeroAtenciones { get; set; }

        [DataType(DataType.Date)]
        [Column("ultima_atencion")]
        public DateTime UltimaAtencion { get; set; }
    }
}
