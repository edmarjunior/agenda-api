using Agenda.Api.Infra;
using Agenda.Api.Models;
using System.Linq;

namespace Agenda.Api.Services.Pacientes
{
    public class PacienteService : IPacienteService
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly Notification _notification;

        public PacienteService(IPacienteRepository pacienteRepository, Notification notification)
        {
            _pacienteRepository = pacienteRepository;
            _notification = notification;
        }

        public Paciente Delete(int id)
        {
            var medico = _pacienteRepository.Get(id);

            if (medico == null)
            {
                _notification.Add("Paciente não encontrado");
                return null;
            }

            _pacienteRepository.Delete(medico);

            if (medico.Telefone != null)
                _pacienteRepository.DeleteTelefone(medico);

            if (medico.Endereco != null)
                _pacienteRepository.DeleteEndereco(medico);

            return medico;
        }

        public Paciente Post(Paciente paciente)
        {
            if (!ValidaDuplicidade(paciente))
                return paciente;

            if (paciente.Telefone != null)
                PostTelefone(paciente);

            _pacienteRepository.Post(paciente);

            return _pacienteRepository.Get(paciente.Id);
        }

        public void Put(Paciente paciente)
        {
            var pacienteDb = _pacienteRepository.Get(paciente.Id);

            if (pacienteDb == null)
            {
                _notification.Add("Paciente não encontrado");
                return;
            }

            if (!ValidaDuplicidade(paciente))
                return;

            _pacienteRepository.Put(paciente);

            // Endereço (atualização)

            if (pacienteDb.Endereco != null)
                _pacienteRepository.DeleteEndereco(pacienteDb);

            if (paciente.Endereco != null)
                _pacienteRepository.PostEndereco(paciente);

            // Telefone (atualização) 

            if (pacienteDb.Telefone != null)
                _pacienteRepository.DeleteTelefone(pacienteDb);

            if (paciente.Telefone != null)
                PostTelefone(paciente);
        }

        private void PostTelefone(Paciente paciente)
        {
            var tipoTelefone = paciente.Telefone?.Tipo;

            if (tipoTelefone == null)
            {
                _notification.AddWithReturn<bool>("Tipo de telefone não informado");
                return;
            }

            _pacienteRepository.PostTelefone(paciente);
        }

        private bool ValidaDuplicidade(Paciente paciente)
        {
            var pacientes = _pacienteRepository.Get().ToList();

            if (pacientes.Any(x => x.Cpf.Equals(paciente.Cpf) && x.Id != paciente.Id))
                return _notification.AddWithReturn<bool>("CPF já cadastrado para outro paciente");

            return true;
        }
    }
}
