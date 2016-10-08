using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using FluentValidation;
using FluentValidation.Results;
using Newtonsoft.Json;
using WebApi.UserManagement.Models;
using WebApiFluentValidation.Models;

namespace WebApi.UserManagement.Validation
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            base.OnActionExecuting(context);
            var parameters = context.ActionArguments.Select(x => x.Value).Where(x => x != null).ToList();
            foreach (var parameter in parameters)
            {
                var argumentType = parameter.GetType();
                var validator = FindValidator(argumentType);
                if (validator != null)
                {
                    var validationResult = validator.Validate(parameter);
                    if (!validationResult.IsValid)
                    {
                        FormatResponse(validationResult);
                    }
                }
            }

            if (parameters.Count < 1)
            {
                ThrowValidationResponseError(new Response {Errors = new List<Error> {new Error {Code = 101, Message = "Invalid request."} } });
            }
        }

        private IValidator FindValidator(Type type)
        {
            // Can be replaced with custom IOC logic
            if (type == typeof(User))
            {
                return new UserValidator();
            }
            return null;
        }

        private void FormatResponse(ValidationResult validationResult)
        {
            var response = new Response { Errors = validationResult.Errors.Select(x => x.CustomState as Error).ToList() };
            ThrowValidationResponseError(response);
        }

        private static void ThrowValidationResponseError(Response response)
        {
            var responseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest)
            {
                Content = new StringContent(JsonConvert.SerializeObject(response, Formatting.Indented))
            };
            throw new HttpResponseException(responseMessage);
        }
    }
}