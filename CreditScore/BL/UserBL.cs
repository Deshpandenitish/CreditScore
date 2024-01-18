using AutoMapper;
using CreditScore.Models.Entities;
using CreditScore.Models.Interface;
using CreditScore.Models.Responses;
using Serilog;
using System.Text;

namespace CreditScore.BL
{
    public class UserBL : IBuisiness
    {
        IUserRepo _userRepo;
        private IMapper _mapper;
        public UserBL(IUserRepo userRepo, IMapper _mapper)
        {
            _userRepo=userRepo ?? throw new ArgumentNullException(nameof(userRepo));
            this._mapper = _mapper;
        }

        private string EncryptPassword(string password)
        {
            if (password == null)
                throw new ArgumentNullException("Plain Text");
            byte[] encData = Encoding.UTF8.GetBytes(password);
            string encodedpwd = Convert.ToBase64String(encData);
            return encodedpwd;
        }

        public async Task<IList<UserResponse>> GetAllUsers()
        {
            try {
                var result = await _userRepo.GetAllUsers();
                return _mapper.Map<IList<UserResponse>>(result);
            }
            catch (Exception ex) {
                Log.Information("Exception ::GetAllUsers:",ex);
                throw;
            }
        }
        public async Task<bool> SaveUserDetails(User user)
        {
            try {
                user.PasswordHash = EncryptPassword(user.PasswordHash);
                return await _userRepo.SaveUserDetails(user);
            }
            catch(Exception ex) {
                Log.Information("Exception ::SaveUserDetails:", ex);
                throw;
            }
        }

        public bool AuthenticateUser(string email,string password)
        {
            password= EncryptPassword(password);
            return _userRepo.ValidateLogin(email, password);
        }
    }
}
