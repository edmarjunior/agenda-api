﻿
namespace Agenda.Api.Models
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
    }
}
