using System.Windows;
using WPF.MVVM.Model;
using WPF.Service;

namespace WPF.MVVM.View
{
    public partial class RegistrationView : Window
    {
        private IUserService _userService;

        public RegistrationView(IUserService userService)
        {
            this._userService = userService;

            InitializeComponent();
        }

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new User
            {
                FirstName = nameTextBox.Text,
                Username = emailTextBox.Text,
                Email = emailTextBox.Text,
                Phone = phoneTextBox.Text,
                LastName = lastnameTextBox.Text,
                Password = pswTextBox.Text,
            };

            try
            {
                await _userService.RegisterUserAsync(newUser);
                MessageBox.Show("Successfully registered!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
