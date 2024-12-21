using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PROYECTO_SWI.Models
{
    public class Usuario
    {
        [Key]
        [Column("id_usuario")]
        public int IdUsuario { get; set; }

        [ForeignKey("Paciente")]
        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Column("username")]
        public string? Username { get; set; }

        [Column("password_hash")]
        public string? PasswordHash { get; set; }
        public Paciente? Paciente { get; set; }
    }
}
