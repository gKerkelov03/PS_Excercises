using System.Windows;
using UI.ViewModels;

namespace UI.Windows
{
    public partial class DisciplineManagementWindow : Window
    {
        public DisciplineManagementWindow(DisciplineManagementViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
} 