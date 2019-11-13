using Agenda.Api.Data.Infra;
using Agenda.Api.Models;
using System.Collections.Generic;

namespace Agenda.Api.Services.Agendamentos
{
    public interface IAgendamentoRepository : IDataBaseTransaction
    {
        IEnumerable<Agendamento> Get(int? idMedico, int? idPaciente = null);
        Agendamento Get(int id);
        void Post(Agendamento gendamento);
        void Put(Agendamento gendamento);
        void Delete(Agendamento agendamento);
    }
}
