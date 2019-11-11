
namespace Agenda.Api.Services
{
    public interface IPessoaService<T> where T : class
    {
        T Delete(int id);
        void Put(T model);
        T Post(T model);
    }
}
