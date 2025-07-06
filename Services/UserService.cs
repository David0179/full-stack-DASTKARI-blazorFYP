using DAL;
using Entities;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly UserDAL _userDal;

        public UserService()
        {
            _userDal = new UserDAL();
        }

        public void AddUser(AppUser user)
        {
            _userDal.AddUser(user);
        }

        public AppUser GetUserByEmail(string email)
        {
            return _userDal.GetUserByEmail(email);
        }

        public int GetTotalUserCount()
        {
            return _userDal.GetTotalUserCount();
        }
    }
}
