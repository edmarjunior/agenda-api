using Agenda.Api.Infra;
using Agenda.Api.Models;
using Agenda.ApiServices.Usuarios;
using System.Linq;

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

            if (usuario.Telefone != null)
                _usuarioRepository.DeleteTelefone(usuario);

            if (usuario.Endereco != null)
                _usuarioRepository.DeleteEndereco(usuario);

            return usuario;
        }

        public Usuario Post(Usuario usuario)
        {
            if (!ValidaDuplicidade(usuario))
                return usuario;

            if (usuario.Telefone != null)
                PostTelefone(usuario);

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

            if (!ValidaDuplicidade(usuario))
                return;

            _usuarioRepository.Put(usuario);

            // Endereço (atualização)

            if (userDb.Endereco != null)
                _usuarioRepository.DeleteEndereco(userDb);

            if (usuario.Endereco != null)
                _usuarioRepository.PostEndereco(usuario);

            // Telefone (atualização) 

            if (userDb.Telefone != null)
                _usuarioRepository.DeleteTelefone(userDb);

            if (usuario.Telefone != null)
                PostTelefone(usuario);
        }

        private void PostTelefone(Usuario usuario)
        {
            var tipoTelefone = usuario.Telefone?.Tipo;

            if (tipoTelefone == null)
            {
                _notification.AddWithReturn<bool>("Tipo de telefone não informado");
                return;
            }

            _usuarioRepository.PostTelefone(usuario);
        }

        private bool ValidaDuplicidade(Usuario usuario)
        {
            var usuarios = _usuarioRepository.Get().ToList();

            if (usuarios.Any(x => x.Cpf.Equals(usuario.Cpf) && x.Id != usuario.Id))
                return _notification.AddWithReturn<bool>("CPF já cadastrado para outro usuário");

            return true;
        }
    }
}
