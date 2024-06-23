using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.MVVM.Model;
using WPF.Service;

namespace WPF.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
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
                Name = nameTextBox.Text,
                Email = emailTextBox.Text,
                PhoneNumber = phoneTextBox.Text,
                LastName = lastnameTextBox.Text,
                Password = pswTextBox.Text,
            };

            await _userService.RegisterUserAsync(newUser);
        }
    }
}
