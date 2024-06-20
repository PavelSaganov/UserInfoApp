using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF.MVVM.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() 
        {
            InitializeComponent();

            FillUserInfoView();
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