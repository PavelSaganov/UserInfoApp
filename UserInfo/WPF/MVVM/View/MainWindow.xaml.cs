using System.Windows;
using WPF.Service;

namespace WPF.MVVM.View
{
    public partial class MainWindow : Window
    {
        private readonly Func<AuthView> _authViewFactory;
        private IUserService _userService;

        public MainWindow(IUserService userService, Func<AuthView> authViewFactory)
        {
            this._userService = userService;
            this._authViewFactory = authViewFactory;

            InitializeComponent();
            FillFrames();
            FillUserInfoView();
        }

        private void FillFrames()
        {
            var authView = _authViewFactory.Invoke();
            this.AuthFrame.Navigate(authView);
        }

        public void FillUserInfoView()
        {
            this.userInfoView.Text = "Please authorize or sign up";
        }

        public void ShowDevInfo_Click(object sender, RoutedEventArgs e)
        {
            this.userInfoView.Text = "Hi my name is Pavel!";
        }
    }
}