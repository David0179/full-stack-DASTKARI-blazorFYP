using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserSession
    {
        private readonly IUserService _userService;
        private AppUser? _currentUser;

        public UserSession(IUserService userService)
        {
            _userService = userService;
        }
        public void ClearSession()
        {
            _currentUser = null;
        }

        public async Task LoadUserAsync(string email)
        {
            // If you want async, you can make this async by wrapping in Task.Run
            _currentUser = await Task.Run(() => _userService.GetUserByEmail(email));

        }

        public AppUser? CurrentUser => _currentUser;
        public int? UserId => _currentUser?.Id;
    }

}
