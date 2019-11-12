using Agenda.Api.Data.Context;

namespace Agenda.Api.Data.Infra
{
    public class DataBaseTransaction : IDataBaseTransaction
    {
        private readonly AgendaContext _context;

        public DataBaseTransaction(AgendaContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _context.Database.BeginTransaction();
        }

        public void CommitTransaction()
        {
            _context.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _context.Database.RollbackTransaction();
        }

    }
}
