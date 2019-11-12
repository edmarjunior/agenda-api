using Agenda.Api.Data.Repository;
using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Medicos;
using System.Collections.Generic;

namespace Agenda.Api.Services.Agendamentos
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly Notification _notification;
        private readonly IMedicoRepository _medicoRepository;

        public AgendamentoService(AgendamentoRepository agendamentoRepository, Notification notification, IMedicoRepository medicoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _notification = notification;
            _medicoRepository = medicoRepository;
        }

        public Agendamento Delete(int id)
        {
            var agendamento = _agendamentoRepository.Get(id);

            if (agendamento == null)
            {
                _notification.Add("Agendamento não encontrado");
                return null;
            }

            return _agendamentoRepository.Delete(id);
        }

        public IEnumerable<Agendamento> Get(int? idMedico) => _agendamentoRepository.Get(idMedico);

        public Agendamento Get(int id) => _agendamentoRepository.Get(id);

        public Agendamento Post(Agendamento agendamento)
        {
            if (!IsValid(agendamento))
                return agendamento;

            return _agendamentoRepository.Post(agendamento);
        }

        public void Put(Agendamento agendamento)
        {
            if (!IsValid(agendamento))
                return;

            _agendamentoRepository.Put(agendamento);
        }

        private bool IsValid(Agendamento agendamento)
        {
            // validando MÉDICO
            var medico = _medicoRepository.Get(agendamento.Medico.Id);
            if (medico == null)
                return _notification.AddWithReturn<bool>("Médico não encontrado");

            // validando PACIENTE
            var paciente = _medicoRepository.Get(agendamento.Paciente.Id);
            if (paciente == null)
                return _notification.AddWithReturn<bool>("Paciente não encontrado");

            return true;
        }
    }
}
