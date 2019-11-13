using Agenda.Api.Data.Context;
using Agenda.Api.Data.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Agenda.Api.Data.Repository
{
    public class PessoaRepository<T> : DataBaseTransaction, IPessoaRepository<T> where T : class
    {
        protected AgendaContext _context;

        public PessoaRepository(AgendaContext context) : base(context)
        {
            _context = context;
        }

        // CRUD

        public IEnumerable<T> Get() => _context.Set<T>().AsNoTracking();

        public T Get(int id) => _context.Set<T>().Find(id);

        public void Post(T model)
        {
            _context.Add(model);
            _context.SaveChanges();
        }

        public void Put(T model)
        {
            _context.Entry(model).Property("EnderecoId").CurrentValue = null;
            _context.Entry(model).Property("TelefoneId").CurrentValue = null;

            _context.Entry(model).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
            _context.SaveChanges();
        }

        // Endereço 

        public void PostEndereco(Pessoa pessoa)
        {
            _context.Add(pessoa.Endereco);
            _context.SaveChanges();
        }

        public void DeleteEndereco(Pessoa pessoa)
        {
            _context.Remove(pessoa.Endereco);
            _context.SaveChanges();
        }

        // Telefone

        public void DeleteTelefone(Pessoa pessoa)
        {
            _context.Remove(pessoa.Telefone);
            _context.SaveChanges();
        }

        public void PostTelefone(Pessoa pessoa)
        {
            //var idTipo = pessoa.Telefone.Tipo.Id;
            //_context.Entry(pessoa.Telefone).Property("TipoId").CurrentValue = pessoa.Telefone.Tipo.Id;
            //pessoa.Telefone.Tipo = null;

            _context.Add(pessoa.Telefone);
            _context.Entry(pessoa.Telefone.Tipo).State = EntityState.Unchanged;
            _context.SaveChanges();
        }
    }
}
