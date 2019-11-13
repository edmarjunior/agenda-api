using Agenda.Api.Dto;
using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Medicos;
using Agenda.Api.Services.Pacientes;
using System.Linq;

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

            _agendamentoRepository.Delete(agendamento);
            return agendamento;
        }

        public void Post(AgendamentoDto agendamentoDto)
        {
            var agendamento = agendamentoDto.ToModel();

            if (!IsValid(agendamento))
                return;

            _agendamentoRepository.Post(agendamento);
            agendamentoDto.Id = agendamento.Id;
        }

        public void Put(AgendamentoDto agendamentoDto)
        {
            var agendamento = agendamentoDto.ToModel();

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

            var agendamentosMedico = _agendamentoRepository.Get(idMedico: medico.Id).ToList();
            if (agendamentosMedico.Any(x => x.Data == agendamento.Data && x.Id != agendamento.Id))
                return _notification.AddWithReturn<bool>("Médico já possui outro agendamento nesse horário");

            agendamento.Medico = medico;

            // validando PACIENTE
            var paciente = _pacienteRepository.Get(agendamento.Paciente.Id);
            if (paciente == null)
                return _notification.AddWithReturn<bool>("Paciente não encontrado");

            var agendamentosPaciente = _agendamentoRepository.Get(null, paciente.Id).ToList();
            if (agendamentosPaciente.Any(x => x.Data == agendamento.Data && x.Id != agendamento.Id))
                return _notification.AddWithReturn<bool>("Paciente já possui outro agendamento nesse horário");

            agendamento.Medico = medico;

            agendamento.Paciente = paciente;

            return true;
        }
    }
}
