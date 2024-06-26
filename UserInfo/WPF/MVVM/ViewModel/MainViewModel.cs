using System.Windows;
using System.Windows.Controls;
using WPF.MVVM.Model;
using WPF.Service;

namespace WPF.MVVM.ViewModel
{
    public class MainViewModel
    {
        public event Action CurrentUserChanged;
        public List<Button> TopPanelButtons { get; set; } = new();
        public User? CurrentUser { get; set; }
        public bool IsUserAuthorized { get; set; } = false;
        public string CurrentUserId { get; set; }
        public string CurrentUserFirstName { get; set; }
        public string CurrentUserLastName { get; set; }
        public string CurrentUserEmail { get; set; }
        public string CurrentUserPhone { get; set; }


        private readonly IUserService _userService;

        public MainViewModel(IUserService userService)
        {
            _userService = userService;
            _userService.CurrentUserChanged += OnCurrentUserChanged;
        }

        public void AddButtonToTopPanel(Button button, Action<object, RoutedEventArgs> onClick)
        {
            button.Click += new RoutedEventHandler(onClick);
            TopPanelButtons.Add(button);
        }

        private void OnCurrentUserChanged(object? sender, User? user)
        {
            CurrentUser = user;
            CurrentUserChanged.Invoke();
        }
    }
}
