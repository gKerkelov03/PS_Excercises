using System.Windows;
using DataLayer.Services;
using UI.ViewModels;
using Welcome.Model;

namespace UI.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(IUserService userService, User currentUser)
    {
        InitializeComponent();
        DataContext = new MainViewModel(userService, currentUser);
    }
}