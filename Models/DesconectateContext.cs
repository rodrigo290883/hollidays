using System;
using mvc_dotnet.Models;
using Microsoft.EntityFrameworkCore;

namespace mvc_dotnet.Data
{
    public class DesconectateContext : DbContext
    {

        public DesconectateContext(DbContextOptions<DesconectateContext> options) : base(options)
        {
        }

        public virtual DbSet<Empleados> Empleados { get; set; }
        public virtual DbSet<solicitudes> solicitudes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleados>().ToTable("Empleados");
            modelBuilder.Entity<solicitudes>().ToTable("solicitudes");
            
        }
    }
}


