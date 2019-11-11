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

        Endereco PostEndereco(Usuario usuario);
        void DeleteEndereco(Usuario usuario);

        Telefone PostTelefone(Usuario usuario, byte idTipo);
        void DeleteTelefone(Usuario usuario);
    }
}
