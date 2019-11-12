using Agenda.Api.Infra;
using Agenda.Api.Models;

namespace Agenda.Api.Services.Medicos
{
    public class MedicoService : IMedicoService
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly Notification _notification;

        public MedicoService(IMedicoRepository medicoRepository, Notification notification)
        {
            _medicoRepository = medicoRepository;
            _notification = notification;
        }

        public Medico Delete(int id)
        {
            var medico = _medicoRepository.Get(id);

            if (medico == null)
            {
                _notification.Add("Médico não encontrado");
                return null;
            }

            _medicoRepository.Delete(medico);

            if (medico.Telefone != null)
                _medicoRepository.DeleteTelefone(medico);

            if (medico.Endereco != null)
                _medicoRepository.DeleteEndereco(medico);

            return medico;
        }

        public Medico Post(Medico medico)
        {
            if (medico.Telefone != null)
                PostTelefone(medico);

            _medicoRepository.Post(medico);

            return _medicoRepository.Get(medico.Id);
        }

        public void Put(Medico medico)
        {
            var medicoDb = _medicoRepository.Get(medico.Id);

            if (medicoDb == null)
            {
                _notification.Add("Médico não encontrado");
                return;
            }

            _medicoRepository.Put(medico);

            // Endereço (atualização)

            if (medicoDb.Endereco != null)
                _medicoRepository.DeleteEndereco(medicoDb);

            if (medico.Endereco != null)
                _medicoRepository.PostEndereco(medico);

            // Telefone (atualização) 

            if (medicoDb.Telefone != null)
                _medicoRepository.DeleteTelefone(medicoDb);

            if (medico.Telefone != null)
                PostTelefone(medico);
        }

        private void PostTelefone(Medico medico)
        {
            var tipoTelefone = medico.Telefone?.Tipo;

            if (tipoTelefone == null)
            {
                _notification.AddWithReturn<bool>("Tipo de telefone não informado");
                return;
            }

            _medicoRepository.PostTelefone(medico);
        }
    }
}
