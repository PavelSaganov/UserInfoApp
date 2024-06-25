using System.Windows;
using System.Windows.Controls;
using WPF.MVVM.Model;
using WPF.Service;

namespace WPF.MVVM.View
{
    public partial class AuthView : Page
    {
        private readonly Func<RegistrationView> _registrationViewFactory;
        private readonly IUserService _userService;

        public AuthView(Func<RegistrationView> registrationViewFactory, IUserService userService)
        {
            _registrationViewFactory = registrationViewFactory;
            _userService = userService;
            _userService.CurrentUserChanged += ChangeLoginBtnVisibility;
            InitializeComponent();
        }

        private void ChangeLoginBtnVisibility(object? sender, User? currentUser)
        {
            if (currentUser is null)
            {
                logoutBtn.Visibility = Visibility.Hidden;
                loginBtn.Visibility = Visibility.Visible;
            }
            else
            {
                logoutBtn.Visibility = Visibility.Visible;
                loginBtn.Visibility = Visibility.Hidden;
            }
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            var registrationView = _registrationViewFactory.Invoke();
            registrationView.Show();
        }

        private async void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _userService.AuthorizeUserAsync(emailInput.Text, pswInput.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _userService.LogoutCurrentUserAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
