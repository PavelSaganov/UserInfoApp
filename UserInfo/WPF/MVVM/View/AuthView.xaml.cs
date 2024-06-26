using System.Windows;
using System.Windows.Controls;
using WPF.MVVM.ViewModel;

namespace WPF.MVVM.View
{
    public partial class AuthView : Page
    {
        private readonly Func<RegistrationView> _registrationViewFactory;
        private readonly AuthViewModel _authViewModel;

        public AuthView(Func<RegistrationView> registrationViewFactory, AuthViewModel authViewModel)
        {
            _registrationViewFactory = registrationViewFactory;
            _authViewModel = authViewModel;
            InitializeComponent();
            DataContext = _authViewModel;
            _authViewModel.CurrentUserChanged += ChangeLoginBtnVisibility;

        }

        private void ChangeLoginBtnVisibility()
        {
            if (logoutBtn.Visibility == Visibility.Visible
                && loginBtn.Visibility == Visibility.Hidden)
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
                await _authViewModel.AuthorizeUserAsync();
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
                await _authViewModel.LogoutCurrentUserAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
