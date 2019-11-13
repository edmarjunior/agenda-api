using Agenda.Api.Dto;
using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Agendamentos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentosController : BaseController
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IAgendamentoService _agendamentoService;

        public AgendamentosController(IAgendamentoRepository agendamentoRepository, IAgendamentoService agendamentoService, Notification notification) : base(notification)
        {
            _agendamentoRepository = agendamentoRepository;
            _agendamentoService = agendamentoService;
        }

        [HttpGet]
        public IActionResult Get(int? idMedico = null, byte page = 1, byte size = 10)
        {
            var agendamentos = _agendamentoRepository.Get(idMedico).ToList();
            var count = agendamentos.Count;

            // aplicando paginação
            agendamentos = agendamentos.Skip((page - 1) * size).Take(size).ToList();

            return Ok(agendamentos.Select(x => ConvertResult(x)), count);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var agendamento = _agendamentoRepository.Get(id);

            return agendamento == null
                ? NotFound()
                : Ok(ConvertResult(agendamento));
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, AgendamentoDto agendamentoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != agendamentoDto.Id)
                return BadRequest("Identificadores do agendamento estão divergentes");

            if (!Exists(id))
                return NotFound();

            _agendamentoRepository.BeginTransaction();

            // editando
            _agendamentoService.Put(agendamentoDto);

            if (_notification.Any)
            {
                _agendamentoRepository.RollbackTransaction();
                return BadRequest();
            }

            _agendamentoRepository.CommitTransaction();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(AgendamentoDto agendamentoDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _agendamentoRepository.BeginTransaction();

            // cadastrando
            _agendamentoService.Post(agendamentoDto);

            if (_notification.Any)
            {
                _agendamentoRepository.RollbackTransaction();
                return BadRequest();
            }

            _agendamentoRepository.CommitTransaction();

            return Created(agendamentoDto);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Exists(id))
                return NotFound();

            _agendamentoRepository.BeginTransaction();

            // excluindo
            var agendamento = _agendamentoService.Delete(id);

            if (_notification.Any)
            {
                _agendamentoRepository.RollbackTransaction();
                return BadRequest();
            }

            _agendamentoRepository.CommitTransaction();

            return Ok(ConvertResult(agendamento));
        }

        private bool Exists(int id) => _agendamentoRepository.Get(id) != null;

        private object ConvertResult(Agendamento ag)
        {
            return new
            {
                ag.Id,
                ag.Data,
                Medico = new
                {
                    ag.Medico.Id,
                    ag.Medico.Nome,
                    ag.Medico.Cpf
                },
                Paciente = new
                {
                    ag.Paciente.Id,
                    ag.Paciente.Nome,
                    ag.Paciente.Cpf
                }
            };
        }
    }
}
