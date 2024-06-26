using WPF.MVVM.Model;
using WPF.Service;

namespace WPF.MVVM.ViewModel
{
    public class AuthViewModel
    {
        public event Action CurrentUserChanged;
        public string Email { get; set; }
        public string Password { get; set; }

        private readonly IUserService _userService;

        public AuthViewModel(IUserService userService)
        {
            _userService = userService;
            _userService.CurrentUserChanged += OnCurrentUserChanged;
        }

        public async Task AuthorizeUserAsync()
        {
            await _userService.AuthorizeUserAsync(Email, Password);
        }

        public async Task LogoutCurrentUserAsync()
        {
            await _userService.LogoutCurrentUserAsync();
        }

        private void OnCurrentUserChanged(object? sender, User? user)
        {
            CurrentUserChanged.Invoke();
        }
    }
}
