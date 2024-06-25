using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.MVVM.ViewModel;
using WPF.Service;

namespace WPF.MVVM.View
{
    public partial class MainWindow : Window
    {
        private readonly Func<AuthView> _authViewFactory;
        private IUserService _userService;

        private MainViewModel _mainViewModel;

        public MainWindow(IUserService userService, Func<AuthView> authViewFactory, MainViewModel mainViewModel)
        {
            this._userService = userService;
            this._authViewFactory = authViewFactory;
            _mainViewModel = mainViewModel;
            _mainViewModel.CurrentUserChanged += GenerateUserInfoFrame;

            InitializeComponent();
            GenerateTopPanelButtons();
            GenerateAuthFrame();

            DataContext = _mainViewModel;
        }

        private void GenerateAuthFrame()
        {
            var authView = _authViewFactory.Invoke();
            this.AuthFrame.Navigate(authView);
        }

        private void GenerateTopPanelButtons()
        {
            var showUserInfoBtn = new Button()
            {
                Content = "Information about current user",
                Height = 30,
            };
            var showDevInfoBtn = new Button()
            {
                Content = "Information about developer",
                Height = 30
            };
            _mainViewModel.AddButtonToTopPanel(showUserInfoBtn, ShowUserInfo_Click);
            _mainViewModel.AddButtonToTopPanel(showDevInfoBtn, ShowDevInfo_Click);
        }

        private void ShowDevInfo_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Children.Clear();
            var userInfoTextBox = new TextBlock { Text = "Hi my name is Pavel!" };
            mainFrame.Children.Add(userInfoTextBox);
        }

        private void ShowUserInfo_Click(object sender, RoutedEventArgs e)
        {
            GenerateUserInfoFrame();
            //FillInfoView(this, _userService.GetCurrentUser());
        }

        private void GenerateUserInfoFrame()
        {
            mainFrame.Children.Clear();
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };

            var userIdTextBlock = new TextBlock();
            var userFirstNameTextBlock = new TextBlock();
            var userLastNameTextBlock = new TextBlock();
            var userEmailTextBlock = new TextBlock();
            var userPhoneTextBlock = new TextBlock();
            if (_mainViewModel.IsUserAuthorized)
            {
                var userIdBinding = new Binding("CurrentUserId") { Source = _mainViewModel };
                var userFirstNameBinding = new Binding("CurrentUserFirstName") { Source = _mainViewModel };
                var userLastNameBinding = new Binding("CurrentUserLastName") { Source = _mainViewModel };
                var userEmailBinding = new Binding("CurrentUserEmail") { Source = _mainViewModel };
                var userPhoneBinding = new Binding("CurrentUserPhone") { Source = _mainViewModel };

                userIdTextBlock.SetBinding(TextBlock.TextProperty, userIdBinding);
                userFirstNameTextBlock.SetBinding(TextBlock.TextProperty, userFirstNameBinding);
                userLastNameTextBlock.SetBinding(TextBlock.TextProperty, userLastNameBinding);
                userEmailTextBlock.SetBinding(TextBlock.TextProperty, userEmailBinding);
                userPhoneTextBlock.SetBinding(TextBlock.TextProperty, userPhoneBinding);

                stackPanel.Children.Add(userIdTextBlock);
                stackPanel.Children.Add(userFirstNameTextBlock);
                stackPanel.Children.Add(userLastNameTextBlock);
                stackPanel.Children.Add(userEmailTextBlock);
                stackPanel.Children.Add(userPhoneTextBlock);
            }
            else
                stackPanel.Children.Add(new TextBlock { Text = "Please authorize or sign up" });

            mainFrame.Children.Add(stackPanel);
        }

        //private void FillInfoView(object? sender, User? user)
        //{
        //    if (user is null)
        //        this.userInfoView.Text = "Please authorize or sign up";
        //    else
        //        this.userInfoView.Text = $"Id: {user.Id} \n Name: {user.FirstName} \n Lastname: {user.LastName} \n Email: {user.Email} \n Phone: {user.Phone}";
        //}
    }
}