using Agenda.Api.Data.Context;
using Agenda.Api.Models;
using Agenda.Api.Services.Usuarios;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Agenda.Api.Data.Repository
{
    public class UsuarioRepository : PessoaRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AgendaContext context) : base(context)
        {

        }

        public new Usuario Get(int id)
        {
            return _context.Usuarios.AsNoTracking()
                .Include(x => x.Endereco)
                .Include(x => x.Telefone).ThenInclude(x => x.Tipo)
                .Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
