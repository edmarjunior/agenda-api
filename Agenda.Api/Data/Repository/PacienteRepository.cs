using Agenda.Api.Data.Context;
using Agenda.Api.Models;
using Agenda.Api.Services.Pacientes;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Agenda.Api.Data.Repository
{
    public class PacienteRepository : PessoaRepository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(AgendaContext context) : base(context)
        {

        }

        public new Paciente Get(int id)
        {
            return _context.Pacientes.AsNoTracking()
                .Include(x => x.Endereco)
                .Include(x => x.Telefone).ThenInclude(x => x.Tipo)
                .Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
