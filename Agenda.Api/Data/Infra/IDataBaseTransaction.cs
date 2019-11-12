namespace Agenda.Api.Data.Infra
{
    public interface IDataBaseTransaction
    {
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
