using WebApiFluentValidation.Models;

namespace WebApi.UserManagement.Repository
{
    public interface IUserRepository
    {
        bool Add(User user);
        User Get(string userName);
    }
}