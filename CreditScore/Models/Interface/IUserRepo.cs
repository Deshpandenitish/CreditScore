using CreditScore.Models.Entities;

namespace CreditScore.Models.Interface
{
    public interface IUserRepo
    {
        Task<IList<User>> GetAllUsers();
        Task<bool> SaveUserDetails(User user);
        bool ValidateLogin(string email, string password);
    }
}
