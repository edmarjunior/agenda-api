using Agenda.Api.Data.Context;
using Agenda.Api.Data.Infra;
using Agenda.Api.Models;
using Agenda.Api.Services.Agendamentos;
using System;
using System.Collections.Generic;

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
            throw new NotImplementedException();
        }

        public Agendamento Get(int id)
        {
            throw new NotImplementedException();
        }

        public Agendamento Post(Agendamento gendamento)
        {
            throw new NotImplementedException();
        }

        public void Put(Agendamento gendamento)
        {
            throw new NotImplementedException();
        }
    }
}
