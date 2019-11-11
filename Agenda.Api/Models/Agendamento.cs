using System;

namespace Agenda.Api.Models
{
    public class Agendamento
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
        public Usuario Usuario { get; set; }
    }
}
