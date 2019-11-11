using System.Linq;
using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Medicos;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicosController : BaseController
    {
        private readonly IMedicoRepository _medicosRepository;
        private readonly IMedicoService _medicoService;

        public MedicosController(IMedicoRepository medicosRepository, Notification notification,
            IMedicoService medicoService) : base(notification)
        {
            _medicosRepository = medicosRepository;
            _medicoService = medicoService;
        }

        [HttpGet]
        public IActionResult Get(byte page = 1, byte size = 10)
        {
            var medicos = _medicosRepository.Get().ToList();
            var count = medicos.Count;

            // aplicando paginação
            medicos = medicos.Skip((page - 1) * size).Take(size).ToList();

            return Ok(medicos, count);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var medico = _medicosRepository.Get(id);

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

            _medicoService.Put(medico);

            return _notification.Any
                ? BadRequest()
                : NoContent();
        }

        [HttpPost]
        public IActionResult Post(Medico medico)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var medicoDb = _medicoService.Post(medico);

            return _notification.Any
                ? BadRequest()
                : Created(medicoDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Exists(id))
                return NotFound();

            var medico = _medicoService.Delete(id);

            return _notification.Any
                ? BadRequest()
                : Ok(medico);
        }

        private bool Exists(int id) => _medicosRepository.Get(id) != null;
    }
}
