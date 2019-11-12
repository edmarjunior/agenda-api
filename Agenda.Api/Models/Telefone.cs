using Newtonsoft.Json;

namespace Agenda.Api.Models
{
    public class Telefone
    {
        [JsonIgnore]
        public int Id { get; set; }
        public byte? Ddd { get; set; }
        public int? Numero { get; set; }
        public TelefoneTipo Tipo { get; set; }
    }
}
