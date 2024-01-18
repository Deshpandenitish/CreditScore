using CreditScore.Models.Entities;
using CreditScore.Models.Interface;
using Microsoft.EntityFrameworkCore;

namespace CreditScore.Repository
{
    public class UserRepo: IUserRepo
    {
        private readonly CreditScoreContext _DBContext;
        private readonly IAudit audit;
        public UserRepo(CreditScoreContext dbContext, IAudit audit)
        {
            _DBContext = dbContext;
            this.audit = audit;
        }
        public async Task<IList<User>> GetAllUsers()
        {
            try {
                var result= await _DBContext.Users.ToListAsync();
                return result;
            }
            catch (Exception ex) 
            { return new List<User>(); }
        }
        public async Task<bool> SaveUserDetails(User user)
        {
            user.CreatedDate = DateTime.Now;
            var entity=_DBContext.Users.Add(user);
            var status = await _DBContext.SaveChangesAsync();

            var auditLog = new AuditTrail
            {
                UserId = entity.Entity.UserId,
                ActivityDescription = "User Created",
                Timestamp = DateTime.Now,
                TableName = nameof(User),
            };
            await audit.addaudit(auditLog);
            return status > 0 ? true : false;
        }
        public bool ValidateLogin(string email, string password)
        {
            var status = _DBContext.Users.Any(res => res.Email == email);
            return status;
        }
    }
}
