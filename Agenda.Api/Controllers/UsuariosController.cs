using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Usuarios;
using Agenda.ApiServices.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioService usuarioService,
            IUsuarioRepository usuarioRepository,
            Notification notification)
            : base(notification)
        {
            _usuarioService = usuarioService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_usuarioRepository.Get());

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var usuario = _usuarioRepository.Get(id);

            return usuario == null
                ? NotFound()
                : Ok(usuario);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != usuario.Id)
                return BadRequest("Identificadores do usuário estão divergentes");

            if (!Exists(id))
                return NotFound();

            _usuarioService.Put(usuario);

            return _notification.Any
                ? BadRequest()
                : NoContent();
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioDb = _usuarioService.Post(usuario);

            return _notification.Any
                ? BadRequest()
                : Created(usuarioDb);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Exists(id))
                return NotFound();

            var usuario = _usuarioService.Delete(id);

            return _notification.Any
                ? BadRequest()
                : Ok(usuario);
        }

        private bool Exists(int id) => _usuarioRepository.Get(id) != null;
    }
}
