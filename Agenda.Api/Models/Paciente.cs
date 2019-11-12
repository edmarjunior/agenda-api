
using System.Collections.Generic;

namespace Agenda.Api.Models
{
    public class Paciente : Pessoa
    {
        public IEnumerable<Agendamento> Agendamentos { get; set; }
    }
}
