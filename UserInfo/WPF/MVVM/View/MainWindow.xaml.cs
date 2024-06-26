using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using WPF.MVVM.ViewModel;

namespace WPF.MVVM.View
{
    public partial class MainWindow : Window
    {
        private readonly Func<AuthView> _authViewFactory;
        private readonly MainViewModel _mainViewModel;

        public MainWindow(Func<AuthView> authViewFactory, MainViewModel mainViewModel)
        {
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
                Margin = new Thickness(5,0,5,0)
            };
            var showDevInfoBtn = new Button()
            {
                Content = "Information about developer",
                Height = 30,
                Margin = new Thickness(5,0,5,0)
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
        }

        private void GenerateUserInfoFrame()
        {
            mainFrame.Children.Clear();
            var stackPanel = new StackPanel { Orientation = Orientation.Vertical };

            if (_mainViewModel.CurrentUser is null)
                stackPanel.Children.Add(new TextBlock { Text = "Please authorize or sign up" });
            else
            {
                var userIdTextBlock = new TextBlock();
                var userFirstNameTextBlock = new TextBlock();
                var userLastNameTextBlock = new TextBlock();
                var userEmailTextBlock = new TextBlock();
                var userPhoneTextBlock = new TextBlock();

                var userIdBinding = new Binding("Id") { Source = _mainViewModel.CurrentUser };
                var userFirstNameBinding = new Binding("FirstName") { Source = _mainViewModel.CurrentUser };
                var userLastNameBinding = new Binding("LastName") { Source = _mainViewModel.CurrentUser };
                var userEmailBinding = new Binding("Email") { Source = _mainViewModel.CurrentUser };
                var userPhoneBinding = new Binding("Phone") { Source = _mainViewModel.CurrentUser };

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

            mainFrame.Children.Add(stackPanel);
        }
    }
}