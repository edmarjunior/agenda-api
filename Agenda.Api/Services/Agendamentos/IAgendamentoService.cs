using Agenda.Api.Dto;
using Agenda.Api.Models;

namespace Agenda.Api.Services.Agendamentos
{
    public interface IAgendamentoService
    {
        void Post(AgendamentoDto agendamentoDto);
        void Put(AgendamentoDto agendamentoDto);
        Agendamento Delete(int id);
    }
}
