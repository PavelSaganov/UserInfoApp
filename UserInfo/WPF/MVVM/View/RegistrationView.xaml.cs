using System.Windows;
using WPF.MVVM.ViewModel;

namespace WPF.MVVM.View
{
    public partial class RegistrationView : Window
    {
        private RegistrationViewModel _registrationViewModel;

        public RegistrationView(RegistrationViewModel registrationViewModel)
        {
            InitializeComponent();
            _registrationViewModel = registrationViewModel;
            DataContext = _registrationViewModel.User;
        }

        private async void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await _registrationViewModel.RegisterUserAsync();
                MessageBox.Show("Successfully registered!");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
