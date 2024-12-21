using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTO_SWI.Models
{
    [Table("Pacientes")]
    public class Paciente
    {
        [Key]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Required]
        [Column("dni")]
        public string? Dni { get; set; }

        [Required]
        [Column("nombre")]
        public string? Nombre { get; set; }

        [Required]
        [Column("apellido")]
        public string? Apellido { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [Column("alergias")]
        public string? Alergias { get; set; }


        [Column("condiciones_preexistentes")]
        public string? CondicionesPreexistentes { get; set; }

        //[ForeignKey("TiposPaciente")]
        [Column("id_tipo")]
        public int IdTipo { get; set; }

        [ForeignKey("IdTipo")]
        public TiposPaciente? TipoPaciente { get; set; }
    }
}
