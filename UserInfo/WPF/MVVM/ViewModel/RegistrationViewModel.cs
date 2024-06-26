using WPF.MVVM.Model;
using WPF.Service;

namespace WPF.MVVM.ViewModel
{
    public class RegistrationViewModel
    {
        public User User { get; set; } = new User();

        private readonly IUserService _userService;

        public RegistrationViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public async Task RegisterUserAsync()
        {
            User.Username = User.Email;
            await _userService.RegisterUserAsync(User);
        }
    }
}
