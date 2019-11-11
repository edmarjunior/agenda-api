using Agenda.Api.Data.Context;
using Agenda.Api.Models;
using Agenda.Api.Services.Usuarios;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Agenda.Api.Data.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AgendaContext _context;

        public UsuarioRepository(AgendaContext context)
        {
            _context = context;
        }

        // CRUD

        public IEnumerable<Usuario> Get() => _context.Usuarios;

        public Usuario Get(int id) => GetUsuario(id);

        public void Post(Usuario usuario)
        {
            _context.Add(usuario);
            _context.SaveChanges();
        }

        public void Put(Usuario usuario)
        {
            _context.Entry(usuario).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
        }

        // Endereço 

        public Endereco PostEndereco(Usuario usuario)
        {
            _context.Add(usuario.Endereco);
            return usuario.Endereco;
        }

        public void DeleteEndereco(Usuario usuario)
        {
            _context.Entry(usuario).Property("TelefoneId").CurrentValue = null;
            _context.Remove(usuario.Endereco);
            _context.SaveChanges();
        }

        // Telefone

        public void DeleteTelefone(Usuario usuario)
        {
            usuario.Telefone.Tipo = null;
            _context.Entry(usuario).Property("TelefoneId").CurrentValue = null;
            //Put(usuario);
            _context.Remove(usuario.Telefone);
            _context.SaveChanges();
        }

        public Telefone PostTelefone(Usuario usuario, byte idTipo)
        {
            _context.Entry(usuario.Telefone).Property("TipoId").CurrentValue = idTipo;
            _context.Add(usuario.Telefone);
            return usuario.Telefone;
        }

        // métodos auxiliares

        private Usuario GetUsuario(int id)
        {
            return _context.Usuarios.AsNoTracking()
                .Include(x => x.Endereco)
                .Include(x => x.Telefone).ThenInclude(x => x.Tipo)
                .Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
