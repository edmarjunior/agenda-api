using Agenda.Api.Data.Context;
using Agenda.Api.Data.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Agendamentos;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Agenda.Api.Data.Repository
{
    public class AgendamentoRepository : DataBaseTransaction, IAgendamentoRepository
    {
        private readonly AgendaContext _context;

        public AgendamentoRepository(AgendaContext context) : base(context)
        {
            _context = context;
        }

        public void Delete(Agendamento agendamento)
        {
            _context.Agendamentos.Remove(agendamento);
            _context.SaveChanges();
        }

        public IEnumerable<Agendamento> Get(int? idMedico, int? idPaciente = null)
        {
            return _context.Agendamentos.AsNoTracking()
                .Include(x => x.Medico)
                .Include(x => x.Paciente)
                .Where(x => x.Medico.Id == (idMedico ?? x.Medico.Id) && x.Paciente.Id == (idPaciente ?? x.Paciente.Id));
        }

        public Agendamento Get(int id)
        {
            return _context.Agendamentos.AsNoTracking()
                .Include(x => x.Medico)
                .Include(x => x.Paciente)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public void Post(Agendamento agendamento)
        {
            _context.Add(agendamento);

            _context.Entry(agendamento.Medico).State = EntityState.Unchanged;
            _context.Entry(agendamento.Paciente).State = EntityState.Unchanged;

            if (agendamento.Medico?.Endereco != null)
                _context.Entry(agendamento.Medico.Endereco).State = EntityState.Unchanged;

            if (agendamento.Medico?.Telefone != null)
                _context.Entry(agendamento.Medico.Telefone).State = EntityState.Unchanged;

            if (agendamento.Medico?.Telefone?.Tipo != null)
                _context.Entry(agendamento.Medico.Telefone.Tipo).State = EntityState.Unchanged;


            if (agendamento.Paciente?.Endereco != null)
                _context.Entry(agendamento.Paciente.Endereco).State = EntityState.Unchanged;

            if (agendamento.Paciente?.Telefone != null)
                _context.Entry(agendamento.Paciente.Telefone).State = EntityState.Unchanged;

            if (agendamento.Paciente?.Telefone?.Tipo != null)
                _context.Entry(agendamento.Paciente.Telefone.Tipo).State = EntityState.Unchanged;

            _context.SaveChanges();
        }

        public void Put(Agendamento agendamento)
        {
            //_context.Agendamentos.Update(agendamento);

            _context.Entry(agendamento).Property("MedicoId").CurrentValue = agendamento.Medico.Id;
            _context.Entry(agendamento).Property("PacienteId").CurrentValue = agendamento.Paciente.Id;

            agendamento.Medico = null;
            agendamento.Paciente = null;

            _context.Entry(agendamento).State = EntityState.Modified;
            _context.SaveChanges();


            //_context.Entry(agendamento).State = EntityState.Modified;
            //_context.Entry(agendamento.Medico).State = EntityState.Unchanged;
            //_context.Entry(agendamento.Paciente).State = EntityState.Unchanged;
            //_context.SaveChanges();
        }
    }
}
