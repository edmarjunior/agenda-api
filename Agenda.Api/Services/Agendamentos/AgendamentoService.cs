using Agenda.Api.Data.Repository;
using Agenda.Api.Models;
using System.Collections.Generic;

namespace Agenda.Api.Services.Agendamentos
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;

        public AgendamentoService(AgendamentoRepository agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        public Agendamento Delete(int id)
        {
            return _agendamentoRepository.Delete(id);
        }

        public IEnumerable<Agendamento> Get(int? idMedico)
        {
            return _agendamentoRepository.Get(idMedico);

        }

        public Agendamento Get(int id)
        {
            return _agendamentoRepository.Get(id);

        }

        public Agendamento Post(Agendamento gendamento)
        {
            return _agendamentoRepository.Post(gendamento);
        }

        public void Put(Agendamento gendamento)
        {
            _agendamentoRepository.Put(gendamento);
        }
    }
}
