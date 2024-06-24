using System.Windows;
using System.Windows.Controls;

namespace WPF.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для AuthView.xaml
    /// </summary>
    public partial class AuthView : Page
    {
        private readonly Func<RegistrationView> _registrationViewFactory;

        public AuthView(Func<RegistrationView> registrationViewFactory)
        {
            _registrationViewFactory = registrationViewFactory;
            InitializeComponent();
        }

        private void RegisterBtn_Click(object sender, RoutedEventArgs e)
        {
            var registrationView = _registrationViewFactory.Invoke();
            registrationView.Show();
        }
    }
}
