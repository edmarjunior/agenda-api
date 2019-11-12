using Agenda.Api.Data.Context;
using Agenda.Api.Data.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Agendamentos;
using Microsoft.EntityFrameworkCore;
using System;
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

        public Agendamento Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Agendamento> Get(int? idMedico)
        {
            return _context.Agendamentos.AsNoTracking()
                .Include(x => x.Medico)
                .Include(x => x.Paciente)
                .Where(x => x.Medico.Id == (idMedico ?? x.Medico.Id));
        }

        public Agendamento Get(int id)
        {
            return _context.Agendamentos.AsNoTracking()
                .Include(x => x.Medico)
                .Include(x => x.Paciente)
                .Where(x => x.Id == id).FirstOrDefault();
        }

        public Agendamento Post(Agendamento agendamento)
        {
            //_context.Entry(agendamento).Property("MedicoId").CurrentValue = agendamento.Medico.Id;
            //_context.Entry(agendamento).Property("PacienteId").CurrentValue = agendamento.Paciente.Id;

            //agendamento.Medico = null;
            //agendamento.Paciente = null;
            _context.Add(agendamento);
            _context.Entry(agendamento.Medico).State = EntityState.Unchanged;
            _context.Entry(agendamento.Paciente).State = EntityState.Unchanged;
            _context.SaveChanges();
            return agendamento;
        }

        public void Put(Agendamento agendamento)
        {
            _context.Entry(agendamento).Property("MedicoId").CurrentValue = agendamento.Medico.Id;
            _context.Entry(agendamento).Property("PacienteId").CurrentValue = agendamento.Paciente.Id;

            agendamento.Medico = null;
            agendamento.Paciente = null;

            _context.Entry(agendamento).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
