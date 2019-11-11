
using System.Collections.Generic;

namespace Agenda.Api.Models
{
    public class Paciente : Pessoa
    {
        public Usuario Usuario { get; set; }
        public IEnumerable<Agendamento> Agendamentos { get; set; }
    }
}
