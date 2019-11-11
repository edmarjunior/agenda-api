using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Agenda.Api.Infra
{
    public class Response
    {
        public Response()
        {

        }

        public Response(HttpStatusCode code)
        {
            Code = code;
            Messages = Enumerable.Empty<string>();
        }

        public Response(HttpStatusCode code, IEnumerable<string> messages) : this(code)
        {
            Messages = messages ?? Enumerable.Empty<string>(); ;
        }

        public HttpStatusCode Code { get; set; }
        public string Status => Code.ToString();
        public IEnumerable<string> Messages { get; set; }
        public int? TotalLength { get; set; }

        public bool Ok => Code == HttpStatusCode.OK;
    }

    public class Response<T> : Response
    {
        public Response()
        {

        }

        public Response(HttpStatusCode code, T content) : base(code)
        {
            Content = content;
        }

        public Response(HttpStatusCode code, T content, int totalLength) : this(code, content)
        {
            TotalLength = totalLength;
        }

        public Response(HttpStatusCode code, T content, IEnumerable<string> messages) : base(code, messages)
        {
            Content = content;
        }

        public T Content { get; set; }
    }
}
