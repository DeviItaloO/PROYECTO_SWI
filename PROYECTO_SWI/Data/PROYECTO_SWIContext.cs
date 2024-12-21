using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PROYECTO_SWI.Models;

namespace PROYECTO_SWI.Data
{
    public class PROYECTO_SWIContext : DbContext
    {
        public PROYECTO_SWIContext (DbContextOptions<PROYECTO_SWIContext> options)
            : base(options)
        {
        }

        public DbSet<PROYECTO_SWI.Models.Paciente> Pacientes { get; set; } = default!;
        public DbSet<PROYECTO_SWI.Models.TiposPaciente> TiposPacientes { get; set; } = default!;
        public DbSet<PROYECTO_SWI.Models.Atencion> Atenciones { get; set; } = default!;
        public DbSet<PROYECTO_SWI.Models.Medicina> Medicinas { get; set; } = default!;
        public DbSet<PROYECTO_SWI.Models.SolicitudMedicina> SolicitudMedicinas { get; set; } = default!;
        public DbSet<PROYECTO_SWI.Models.Reporte> Reportes { get; set; } = default!;
        public DbSet<PROYECTO_SWI.Models.HistorialPaciente> HistorialPacientes { get; set; } = default!;

        public DbSet<PROYECTO_SWI.Models.Usuario> Usuarios { get; set; } = default!;
    }
}
