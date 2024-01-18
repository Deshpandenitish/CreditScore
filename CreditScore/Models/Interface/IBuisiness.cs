using CreditScore.Models.Entities;
using CreditScore.Models.Responses;

namespace CreditScore.Models.Interface
{
    public interface IBuisiness
    {
        Task<bool> SaveUserDetails(User user);
        bool AuthenticateUser(string email, string password);
        Task<IList<UserResponse>> GetAllUsers();
    }
}
