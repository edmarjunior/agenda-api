using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Usuarios;
using Agenda.ApiServices.Usuarios;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Agenda.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuariosController(IUsuarioService usuarioService, IUsuarioRepository usuarioRepository, Notification notification) : base(notification)
        {
            _usuarioService = usuarioService;
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public IActionResult Get(string nome = null, byte page = 1, byte size = 10)
        {
            var usuarios = _usuarioRepository.Get().ToList();

            if (!string.IsNullOrEmpty(nome))
                usuarios = usuarios.Where(x => x.Nome.Contains(nome)).ToList();

            var count = usuarios.Count;

            // aplicando paginação
            usuarios = usuarios.Skip((page - 1) * size).Take(size).ToList();

            return Ok(usuarios, count);
        }

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


            _usuarioRepository.BeginTransaction();

            // editando
            _usuarioService.Put(usuario);

            if (_notification.Any)
            {
                _usuarioRepository.RollbackTransaction();
                return BadRequest();
            }

            _usuarioRepository.CommitTransaction();

            return NoContent();
        }

        [HttpPost]
        public IActionResult Post(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _usuarioRepository.BeginTransaction();

            // cadastrando
            usuario = _usuarioService.Post(usuario);

            if (_notification.Any)
            {
                _usuarioRepository.RollbackTransaction();
                return BadRequest();
            }

            _usuarioRepository.CommitTransaction();

            return Created(usuario);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!Exists(id))
                return NotFound();

            _usuarioRepository.BeginTransaction();

            // excluindo
            var usuario = _usuarioService.Delete(id);

            if (_notification.Any)
            {
                _usuarioRepository.RollbackTransaction();
                return BadRequest();
            }

            _usuarioRepository.CommitTransaction();

            return Ok(usuario);
        }

        private bool Exists(int id) => _usuarioRepository.Get(id) != null;
    }
}
