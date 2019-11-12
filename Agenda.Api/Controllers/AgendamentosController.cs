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

            return Ok(agendamentos, count);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var agendamento = _agendamentoService.Get(id);

            return agendamento == null
                ? NotFound()
                : Ok(agendamento);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Agendamento agendamento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != agendamento.Id)
                return BadRequest("Identificadores do usuário estão divergentes");

            if (!Exists(id))
                return NotFound();

            _agendamentoRepository.BeginTransaction();

            // editando
            _agendamentoService.Put(agendamento);

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
            var agendamento = _agendamentoService.Post(agendamentoDto);

            if (_notification.Any)
            {
                _agendamentoRepository.RollbackTransaction();
                return BadRequest();
            }

            _agendamentoRepository.CommitTransaction();

            return Created(agendamento);
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

            return Ok(agendamento);
        }

        private bool Exists(int id) => _agendamentoRepository.Get(id) != null;

    }
}