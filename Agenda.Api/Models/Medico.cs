
using System.Collections.Generic;

namespace Agenda.Api.Models
{
    public class Medico : Pessoa
    {
        public IEnumerable<Agendamento> Agendamentos { get; set; }
    }
}
