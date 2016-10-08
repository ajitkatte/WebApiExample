using FluentValidation;
using WebApiFluentValidation.Models;

namespace WebApi.UserManagement.Validation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithState(x => new Error
                {
                    Code = 101,
                    Message = "FirstName cannot be null or empty."
                });
            RuleFor(x => x.LastName)
                .NotEmpty().WithState(x => new Error
                {
                    Code = 102,
                    Message = "LastName cannot be null or empty."
                });
            RuleFor(x => x.City)
                .NotEmpty().WithState(x => new Error
                {
                    Code = 103,
                    Message = "City cannot be null or empty."
                });

            //RuleFor(x => x.PhoneNumber)
            //    .NotEmpty().WithState(x => new Error
            //    {
            //        Code = 103,
            //        Message = "PhoneNumber cannot be null or empty."
            //    }).Must(x => Regex.IsMatch(@"^(\+[0-9]{9})$", x.ToString())).WithState(x => new Error
            //    {
            //        Code = 104,
            //        Message = "Invalid phone number."
            //    });
        }
    }
}