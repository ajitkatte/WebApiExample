using System.Web.Http;
using WebApi.UserManagement.Logger;
using WebApi.UserManagement.Models;
using WebApi.UserManagement.Repository;
using WebApiFluentValidation.Models;

namespace WebApi.UserManagement.Controllers
{
    [RoutePrefix("api/v1/user")]
    public class UserController : ApiController
    {
        private IUserRepository _userRepository = null;
        private ILogger _logger = null; 

        public UserController(IUserRepository userRepository, ILogger logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [Route("add")]
        [HttpPost]
        public Response Add([FromBody] User user)
        {
            
            var status = _userRepository.Add(user);
            _logger.Log("Add user status "+status);
            return new Response
            {
                Status = status
            };
        }
    }
}