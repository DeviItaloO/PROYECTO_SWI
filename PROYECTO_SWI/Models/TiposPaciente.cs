using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTO_SWI.Models
{
    [Table("Tipos_Paciente")]
    public class TiposPaciente
    {
        [Key]
        [Column("id_tipo")]
        public int IdTipo { get; set; }

        [Required]
        [Column("tipo")]
        public string? Tipo { get; set; }
        public ICollection<Paciente>? Pacientes { get; set; }
    }
}
