using Microsoft.Windows.Themes;
using System.Windows;
using System.Windows.Controls;
using WPF.MVVM.Model;
using WPF.Service;

namespace WPF.MVVM.View
{
    public partial class MainWindow : Window
    {
        private readonly Func<AuthView> _authViewFactory;
        private IUserService _userService;
        private List<Button> topPanelButtons = new();

        public MainWindow(IUserService userService, Func<AuthView> authViewFactory)
        {
            this._userService = userService;
            this._authViewFactory = authViewFactory;

            InitializeComponent();
            CreateTopPanelButtons();
            CreateFrames();
            FillUserInfo();
        }

        public void ShowDevInfo_Click(object sender, RoutedEventArgs e)
        {
            this.userInfoView.Text = "Hi my name is Pavel!";
        }

        public void ShowUserInfo_Click(object sender, RoutedEventArgs e)
        {
            FillInfoView(this, _userService.GetCurrentUser());
        }

        private void FillUserInfo()
        {
            _userService.CurrentUserChanged += FillInfoView;
            this.userInfoView.Text = "Please authorize or sign up";
        }

        private void CreateFrames()
        {
            var authView = _authViewFactory.Invoke();
            this.AuthFrame.Navigate(authView);
        }

        private void CreateTopPanelButtons()
        {
            var showUserInfoBtn = new Button()
            {
                Name = "ShowUserInfo",
                Content = "Information about current user",
                Height = 30
            };
            showUserInfoBtn.Click += new RoutedEventHandler(ShowUserInfo_Click);
            var showDevInfoBtn = new Button()
            {
                Name = "ShowDevInfo",
                Content = "Information about developer",
                Height = 30
            };
            showDevInfoBtn.Click += new RoutedEventHandler(ShowDevInfo_Click);

            topPanelButtons.AddRange(new List<Button> { showUserInfoBtn, showDevInfoBtn });
            topPanelButtons.ForEach(button => TopButtonPanel.Children.Add(button));
        }

        private void FillInfoView(object? sender, User? user)
        {
            if (user is null)
                this.userInfoView.Text = "Please authorize or sign up";
            else
                this.userInfoView.Text = $"Id: {user.Id} \n Name: {user.FirstName} \n Lastname: {user.LastName} \n Email: {user.Email} \n Phone: {user.Phone}";
        }
    }
}