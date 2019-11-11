using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                _notification.Add("Usuario não encontrado");
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
            var userDb = _medicoRepository.Get(medico.Id);

            if (userDb == null)
            {
                _notification.Add("Usuario não encontrado");
                return;
            }

            _medicoRepository.Put(medico);

            // Endereço (atualização)

            if (userDb.Endereco != null)
                _medicoRepository.DeleteEndereco(userDb);

            if (medico.Endereco != null)
                _medicoRepository.PostEndereco(medico);

            // Telefone (atualização) 

            if (userDb.Telefone != null)
                _medicoRepository.DeleteTelefone(userDb);

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
