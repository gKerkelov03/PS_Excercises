using System.Windows;
using DataLayer.Model;

namespace UI.Windows
{
    public partial class DisciplineEditWindow : Window
    {
        private readonly Discipline _discipline;

        public DisciplineEditWindow(Discipline discipline)
        {
            InitializeComponent();
            _discipline = discipline;
            DataContext = discipline;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 