using Agenda.Api.Data.Context;
using Agenda.Api.Models;
using Agenda.Api.Services.Medicos;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Agenda.Api.Data.Repository
{
    public class MedicoRepository : PessoaRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(AgendaContext context) : base(context)
        {

        }

        public new Medico Get(int id)
        {
            return _context.Medicos.AsNoTracking()
                .Include(x => x.Endereco)
                .Include(x => x.Telefone).ThenInclude(x => x.Tipo)
                .Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
