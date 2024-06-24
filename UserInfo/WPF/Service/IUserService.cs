using WPF.MVVM.Model;

namespace WPF.Service
{
    public interface IUserService
    {
        public User? GetCurrentUser();
        public Task RegisterUserAsync(User user);
        public Task AuthorizeUserAsync(string username, string password);
        public Task LogoutCurrentUserAsync();
        public Task<User> GetUserAsync(string username);
    }
}
