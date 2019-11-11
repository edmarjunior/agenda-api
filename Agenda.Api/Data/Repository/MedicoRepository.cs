using Agenda.Api.Data.Context;
using Agenda.Api.Models;
using Agenda.Api.Services.Medicos;

namespace Agenda.Api.Data.Repository
{
    public class MedicoRepository : PessoaRepository<Medico>, IMedicoRepository
    {
        public MedicoRepository(AgendaContext context) :base(context)
        {

        }
    }
}
