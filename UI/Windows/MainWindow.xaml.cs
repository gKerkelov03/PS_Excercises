using System.Windows;
using DataLayer.Services;
using UI.ViewModels;
using Welcome.Model;
using DataLayer.Database;

namespace UI.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(IUserService userService, DatabaseContext dbContext, User currentUser)
    {
        InitializeComponent();
        DataContext = new MainViewModel(userService, dbContext, currentUser);
    }
}