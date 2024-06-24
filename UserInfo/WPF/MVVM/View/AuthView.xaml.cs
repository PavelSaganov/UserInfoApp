using System.Windows;
using System.Windows.Controls;
using WPF.Service;

namespace WPF.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : Page
    {
        private readonly Func<RegistrationView> _registrationViewFactory;
        private readonly IUserService _userService;

        public AuthView(Func<RegistrationView> registrationViewFactory, IUserService userService)
        {
            _registrationViewFactory = registrationViewFactory;
            _userService = userService;
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            var registrationView = _registrationViewFactory.Invoke();
            registrationView.Show();
        }

        private async void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            await _userService.AuthorizeUserAsync(emailInput.Text, pswInput.Text);
        }
    }
}
