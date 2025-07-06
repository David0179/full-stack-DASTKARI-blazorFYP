using Entities;

namespace Services
{
    public interface IUserService
    {
        void AddUser(AppUser user);
        AppUser GetUserByEmail(string email);
        int GetTotalUserCount();
    }
}
