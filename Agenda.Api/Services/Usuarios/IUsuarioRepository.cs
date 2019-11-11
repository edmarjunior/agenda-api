using Agenda.Api.Models;
using System.Collections.Generic;

namespace Agenda.Api.Services.Usuarios
{
    public interface IUsuarioRepository
    {
        IEnumerable<Usuario> Get();
        Usuario Get(int id);
        void Post(Usuario usuario);
        void Put(Usuario usuario);
        void Delete(Usuario usuario);

        void PostEndereco(Usuario usuario);
        void DeleteEndereco(Usuario usuario);

        void PostTelefone(Usuario usuario);
        void DeleteTelefone(Usuario usuario);
    }
}
