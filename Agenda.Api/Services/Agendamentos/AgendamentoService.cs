using Agenda.Api.Dto;
using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Medicos;
using Agenda.Api.Services.Pacientes;
using System.Collections.Generic;

namespace Agenda.Api.Services.Agendamentos
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly Notification _notification;
        private readonly IMedicoRepository _medicoRepository;
        private readonly IPacienteRepository _pacienteRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository,
            Notification notification,
            IMedicoRepository medicoRepository,
            IPacienteRepository pacienteRepository)
        {
            _agendamentoRepository = agendamentoRepository;
            _notification = notification;
            _medicoRepository = medicoRepository;
            _pacienteRepository = pacienteRepository;
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

        public Agendamento Post(AgendamentoDto agendamentoDto)
        {

            var agendamento = agendamentoDto.ToModel();

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
            var paciente = _pacienteRepository.Get(agendamento.Paciente.Id);
            if (paciente == null)
                return _notification.AddWithReturn<bool>("Paciente não encontrado");

            return true;
        }
    }
}
