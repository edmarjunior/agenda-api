using Agenda.Api.Dto;
using Agenda.Api.Models;
using System.Collections.Generic;

namespace Agenda.Api.Services.Agendamentos
{
    public interface IAgendamentoService
    {
        IEnumerable<Agendamento> Get(int? idMedico);
        Agendamento Get(int id);
        Agendamento Post(AgendamentoDto agendamentoDto);
        void Put(Agendamento agendamento);
        Agendamento Delete(int id);
    }
}
