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

        [HttpPost]
        public IActionResult Post(Medico medico)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioDb = _medicoService.Post(medico);

            return _notification.Any
                ? BadRequest()
                : Created(usuarioDb);
        }
    }
}
