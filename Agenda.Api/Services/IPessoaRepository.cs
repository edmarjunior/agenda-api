using Agenda.Api.Data.Infra;
using Agenda.Api.Models;
using System.Collections.Generic;

namespace Agenda.Api.Services
{
    public interface IPessoaRepository<T> : IDataBaseTransaction where T : class
    {
        IEnumerable<T> Get();
        T Get(int id);
        void Post(T model);
        void Put(T model);
        void Delete(T model);

        void PostEndereco(Pessoa pessoa);
        void DeleteEndereco(Pessoa pessoa);
        void DeleteTelefone(Pessoa pessoa);
        void PostTelefone(Pessoa pessoa);
    }
}
