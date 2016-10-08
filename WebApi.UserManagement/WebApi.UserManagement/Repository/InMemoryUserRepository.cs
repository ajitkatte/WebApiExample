using System.Collections.Generic;
using System.Linq;
using WebApiFluentValidation.Models;

namespace WebApi.UserManagement.Repository
{
    public class InMemoryUserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>(); 

        public bool Add(User user)
        {
            if (_users.Any(x => x.FirstName == user.FirstName || x.LastName == user.LastName) == false)
            {
                _users.Add(user);
                return true;
            }
            return false;
        }

        public User Get(string firstName)
        {
            return _users.Find(p => p.FirstName == firstName);
        }
    }
}