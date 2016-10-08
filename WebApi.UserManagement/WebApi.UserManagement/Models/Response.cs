using System.Collections.Generic;
using WebApiFluentValidation.Models;

namespace WebApi.UserManagement.Models
{
    public class Response
    {
        public bool Status { get; set; }

        public List<Error> Errors { get; set; } 
    }
}