using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Pacientes;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacientesController : BaseController
    {
        private readonly IPacienteRepository _pacienteRepository;
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteRepository pacienteRepository, Notification notification, IPacienteService pacienteService) : base(notification)
        {
            _pacienteRepository = pacienteRepository;
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public IActionResult Get(byte page = 1, byte size = 10)
        {
            var pacientes = _pacienteRepository.Get().ToList();
            var count = pacientes.Count;

            // aplicando paginação
            pacientes = pacientes.Skip((page - 1) * size).Take(size).ToList();

            return Ok(pacientes, count);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var paciente = _pacienteRepository.Get(id);

            return paciente == null
                ? NotFound()
                : Ok(paciente);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Paciente paciente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != paciente.Id)
                return BadRequest("Identificadores do usuário estão divergentes");

            if (!Exists(id))
                return NotFound();

            _pacienteRepository.BeginTransaction();

            // editando
            _pacienteService.Put(paciente);

            if (_notification.Any)
            {
                _pacienteRepository.RollbackTransaction();
                return BadRequest();
            }

            _pacienteRepository.CommitTransaction();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(Paciente paciente)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            // cadastrando
            paciente = _pacienteService.Post(paciente);

            if (_notification.Any)
            {
                _pacienteRepository.RollbackTransaction();
                return BadRequest();
            }

            _pacienteRepository.CommitTransaction();

            return Created(paciente);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Exists(id))
                return NotFound();

            // excluindo
            var paciente = _pacienteService.Delete(id);

            if (_notification.Any)
            {
                _pacienteRepository.RollbackTransaction();
                return BadRequest();
            }

            _pacienteRepository.CommitTransaction();

            return Ok(paciente);
        }

        private bool Exists(int id) => _pacienteRepository.Get(id) != null;
    }
}
