
using System.ComponentModel.DataAnnotations;

namespace Agenda.Api.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O Nome é obrigatório")]
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
        public Telefone Telefone { get; set; }
    }
}
