using Agenda.Api.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Agenda.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly Notification _notification;

        public BaseController(Notification notification)
        {
            _notification = notification;
        }

        protected IActionResult Content(HttpStatusCode statusCode, IEnumerable<string> messages)
        {
            HttpContext.Response.StatusCode = (int)statusCode;
            var content = JsonConvert.SerializeObject(new Response(statusCode, messages));
            return base.Content(content);
        }

        protected IActionResult Content<T>(HttpStatusCode statusCode, T content, int totalLength)
        {
            HttpContext.Response.StatusCode = (int)statusCode;
            var obj = new Response<T>(statusCode, content, totalLength);
            return base.Content(JsonConvert.SerializeObject(obj));
        }

        protected IActionResult Content<T>(HttpStatusCode statusCode, T content, IEnumerable<string> messages)
        {
            HttpContext.Response.StatusCode = (int)statusCode;
            var obj = new Response<T>(statusCode, content, messages);
            return base.Content(JsonConvert.SerializeObject(obj));
        }

        protected new IActionResult Ok() => base.Ok(new Response(HttpStatusCode.OK));

        protected IActionResult Ok<T>(T content) => Content(HttpStatusCode.OK, content, null);

        protected IActionResult Ok<T>(T content, int totalLength) => Content(HttpStatusCode.OK, content, totalLength);

        protected IActionResult Created<T>(T content) => Content(HttpStatusCode.Created, content, null);

        protected new IActionResult BadRequest(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(x => x.Errors).ToList();
            errors.ForEach(x => _notification.Add(x.ErrorMessage));
            return Content(HttpStatusCode.BadRequest, _notification.Get);
        }

        protected IActionResult BadRequest(string message = null)
        {
            var messages = new List<string>();

            if (!string.IsNullOrEmpty(message))
                messages = new[] { message }.ToList();
            else if (_notification.Any)
                messages = _notification.Get.ToList();

            return Content(HttpStatusCode.BadRequest, messages);
        }

        protected new IActionResult NotFound() => Content(HttpStatusCode.NotFound, null);
    }
}
