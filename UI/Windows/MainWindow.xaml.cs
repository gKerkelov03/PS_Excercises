using System.Windows;
using DataLayer.Services;
using UI.ViewModels;
using DataLayer.Model;
using DataLayer.Database;

namespace UI.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(IUserService userService, DatabaseContext dbContext, DatabaseUser currentUser)
    {
        InitializeComponent();
        DataContext = new MainViewModel(userService, dbContext, currentUser);
    }
}