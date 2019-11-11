using Agenda.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Api.Data.Context
{
    public class AgendaContext : DbContext
    {
        public AgendaContext(DbContextOptions<AgendaContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<TelefoneTipo> TelefoneTipos { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
    }
}
