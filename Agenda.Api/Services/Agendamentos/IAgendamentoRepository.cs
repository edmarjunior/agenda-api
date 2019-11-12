using Agenda.Api.Data.Infra;
using Agenda.Api.Models;
using System.Collections.Generic;

namespace Agenda.Api.Services.Agendamentos
{
    public interface IAgendamentoRepository : IDataBaseTransaction
    {
        IEnumerable<Agendamento> Get(int? idMedico);
        Agendamento Get(int id);
        Agendamento Post(Agendamento gendamento);
        void Put(Agendamento gendamento);
        Agendamento Delete(int id);
    }
}
