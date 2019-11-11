using Agenda.Api.Models;

namespace Agenda.ApiServices.Usuarios
{
    public interface IUsuarioService
    {
        Usuario Delete(int id);
        void Put(Usuario usuario);
        Usuario Post(Usuario usuario);
    }
}
