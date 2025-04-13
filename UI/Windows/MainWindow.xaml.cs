using System.Windows;
using DataLayer.Services;
using DataLayer.Database;
using DataLayer.Model;
using UI.ViewModels;
using DataLayer;

namespace UI.Windows;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(IUserService userService, DatabaseContext dbContext, DatabaseUser currentUser)
    {
        InitializeComponent();
        var logger = new Logger(dbContext);
        DataContext = new MainViewModel(userService, dbContext, currentUser, logger);
    }
}