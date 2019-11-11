using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.ApiServices.Usuarios;

namespace Agenda.Api.Services.Usuarios
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly Notification _notification;

        public UsuarioService(IUsuarioRepository usuarioRepository, Notification notification)
        {
            _usuarioRepository = usuarioRepository;
            _notification = notification;
        }

        public Usuario Delete(int id)
        {
            var usuario = _usuarioRepository.Get(id);

            if (usuario == null)
            {
                _notification.Add("Usuario não encontrado");
                return null;
            }

            _usuarioRepository.Delete(usuario);

            _usuarioRepository.DeleteTelefone(usuario);

            _usuarioRepository.DeleteEndereco(usuario);

            return usuario;
        }

        public Usuario Post(Usuario usuario)
        {
            if (usuario.Telefone != null && !PostTelefone(usuario))
                return null;

            _usuarioRepository.Post(usuario);

            return _usuarioRepository.Get(usuario.Id);
        }

        public void Put(Usuario usuario)
        {
            var userDb = _usuarioRepository.Get(usuario.Id);

            if (userDb == null)
            {
                _notification.Add("Usuario não encontrado");
                return;
            }

            // Endereço (cadastro)
            if (userDb.Endereco == null && usuario.Endereco != null)
                usuario.Endereco = _usuarioRepository.PostEndereco(usuario);

            // Telefone (cadastro) 
            if (userDb.Telefone == null && usuario.Telefone != null)
            {
                if (!PostTelefone(usuario))
                    return;
            }

            _usuarioRepository.Put(usuario);

            // Endereço (remoção)
            if (userDb.Endereco != null && usuario.Endereco == null)
                _usuarioRepository.DeleteEndereco(userDb);

            // Telefone (remoção) 
            if (userDb.Telefone != null && usuario.Telefone == null)
                _usuarioRepository.DeleteTelefone(userDb);

        }

        private bool PostTelefone(Usuario usuario)
        {
            var tipoTelefone = usuario.Telefone?.Tipo;

            if (tipoTelefone == null)
                return _notification.AddWithReturn<bool>("Tipo de telefone não informado");

            usuario.Telefone.Tipo = null;
            usuario.Telefone = _usuarioRepository.PostTelefone(usuario, tipoTelefone.Id);
            return true;
        }
    }
}
