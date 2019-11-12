using Agenda.Api.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Api.Dto
{
    public class AgendamentoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo data é obrigatório")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "O campo medicoId é obrigatório")]
        public int? MedicoId { get; set; }

        [Required(ErrorMessage = "O campo pacienteId é obrigatório")]
        public int? PacienteId { get; set; }

        public Agendamento ToModel()
        {
            return new Agendamento
            {
                Id = Id,
                Data = Data ?? default,
                Medico = new Medico { Id = MedicoId ?? default },
                Paciente = new Paciente { Id = PacienteId ?? default },
            };
        }
    }
}
