using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CRUDTrabajadores.Models;

namespace CRUDTrabajadores.Data
{
    public class ContextoBD : DbContext
    {
        public ContextoBD (DbContextOptions<ContextoBD> options)
            : base(options)
        {
        }

        public DbSet<Trabajador> Trabajador { get; set; } = default!;
        public DbSet<Departamento> Departamento { get; set; } = default!;
        public DbSet<Distrito> Distrito { get; set; } = default!;
        public DbSet<Provincia> Provincia { get; set; } = default!;
    }
}
