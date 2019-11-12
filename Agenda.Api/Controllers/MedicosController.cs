using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Medicos;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : BaseController
    {
        private readonly IMedicoRepository _medicoRepository;
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoRepository medicosRepository, Notification notification, IMedicoService medicoService) : base(notification)
        {
            _medicoRepository = medicosRepository;
            _medicoService = medicoService;
        }

        [HttpGet]
        public IActionResult Get(byte page = 1, byte size = 10)
        {
            var medicos = _medicoRepository.Get().ToList();
            var count = medicos.Count;

            // aplicando paginação
            medicos = medicos.Skip((page - 1) * size).Take(size).ToList();

            return Ok(medicos, count);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var medico = _medicoRepository.Get(id);

            return medico == null
                ? NotFound()
                : Ok(medico);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Medico medico)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != medico.Id)
                return BadRequest("Identificadores do usuário estão divergentes");

            if (!Exists(id))
                return NotFound();

            _medicoRepository.BeginTransaction();

            // editando
            _medicoService.Put(medico);

            if (_notification.Any)
            {
                _medicoRepository.RollbackTransaction();
                return BadRequest();
            }

            _medicoRepository.CommitTransaction();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(Medico medico)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _medicoRepository.BeginTransaction();

            // cadastrando
            medico = _medicoService.Post(medico);

            if (_notification.Any)
            {
                _medicoRepository.RollbackTransaction();
                return BadRequest();
            }

            _medicoRepository.CommitTransaction();

            return Created(medico);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Exists(id))
                return NotFound();

            _medicoRepository.BeginTransaction();

            // excluindo
            var medico = _medicoService.Delete(id);

            if (_notification.Any)
            {
                _medicoRepository.RollbackTransaction();
                return BadRequest();
            }

            _medicoRepository.CommitTransaction();

            return Ok(medico);
        }

        private bool Exists(int id) => _medicoRepository.Get(id) != null;
    }
}
