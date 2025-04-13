using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataLayer.Model;
using DataLayer.Services;
using UI.Commands;
using UI.Windows;

namespace UI.ViewModels
{
    public class DisciplineViewModel : ViewModelBase
    {
        private readonly IDisciplineService _disciplineService;
        private ObservableCollection<Discipline> _disciplines;
        private Discipline _selectedDiscipline;
        private string _searchText = string.Empty;
        private string _statisticsText = string.Empty;

        public DisciplineViewModel(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
            _disciplines = new ObservableCollection<Discipline>();
            
            AddDisciplineCommand = new AddDisciplineCommand(this);
            EditDisciplineCommand = new EditDisciplineCommand(this);
            DeleteDisciplineCommand = new DeleteDisciplineCommand(this);
            RefreshCommand = new RefreshCommand(this);
            
            _ = LoadDisciplinesAsync();
        }

        public ObservableCollection<Discipline> Disciplines
        {
            get => _disciplines;
            set
            {
                _disciplines = value;
                OnPropertyChanged();
            }
        }

        public Discipline SelectedDiscipline
        {
            get => _selectedDiscipline;
            set
            {
                _selectedDiscipline = value;
                OnPropertyChanged();
            }
        }

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                FilterDisciplines();
            }
        }

        public string StatisticsText
        {
            get => _statisticsText;
            set
            {
                _statisticsText = value;
                OnPropertyChanged();
            }
        }

        public AddDisciplineCommand AddDisciplineCommand { get; }
        public EditDisciplineCommand EditDisciplineCommand { get; }
        public DeleteDisciplineCommand DeleteDisciplineCommand { get; }
        public RefreshCommand RefreshCommand { get; }

        public async void ExecuteAddDiscipline(object parameter)
        {
            var discipline = new Discipline
            {
                Name = "New Discipline",
                Description = "Description",
                Year = 1,
                Semester = 1,
                Credits = 6,
                Lecturer = "Lecturer",
                MaxStudents = 30
            };

            try
            {
                await _disciplineService.CreateDisciplineAsync(discipline);
                await LoadDisciplinesAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding discipline: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void ExecuteEditDiscipline(object parameter)
        {
            if (SelectedDiscipline == null) return;

            var editWindow = new DisciplineEditWindow(SelectedDiscipline);
            if (editWindow.ShowDialog() == true)
            {
                try
                {
                    await _disciplineService.UpdateDisciplineAsync(SelectedDiscipline);
                    await LoadDisciplinesAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating discipline: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public async void ExecuteDeleteDiscipline(object parameter)
        {
            if (SelectedDiscipline == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete {SelectedDiscipline.Name}?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _disciplineService.DeleteDisciplineAsync(SelectedDiscipline.Id);
                    SelectedDiscipline = null;
                    await LoadDisciplinesAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting discipline: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public async void ExecuteRefresh(object parameter)
        {
            await LoadDisciplinesAsync();
        }

        private async Task LoadDisciplinesAsync()
        {
            try
            {
                var disciplines = await _disciplineService.GetAllDisciplinesAsync();
                Disciplines = new ObservableCollection<Discipline>(disciplines);
                await UpdateStatisticsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading disciplines: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilterDisciplines()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                _ = LoadDisciplinesAsync();
                return;
            }

            var filtered = Disciplines.Where(d =>
                d.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                d.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                d.Lecturer.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

            Disciplines = new ObservableCollection<Discipline>(filtered);
        }

        private async Task UpdateStatisticsAsync()
        {
            try
            {
                var yearStats = await _disciplineService.GetDisciplineStatisticsByYearAsync();
                var semesterStats = await _disciplineService.GetDisciplineStatisticsBySemesterAsync();
                var total = await _disciplineService.GetTotalDisciplinesCountAsync();

                StatisticsText = $"Total Disciplines: {total}\n\n" +
                               "Disciplines per Year:\n" +
                               string.Join("\n", yearStats.Select(kvp => $"Year {kvp.Key}: {kvp.Value}")) +
                               "\n\nDisciplines per Semester:\n" +
                               string.Join("\n", semesterStats.Select(kvp => $"Semester {kvp.Key}: {kvp.Value}"));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating statistics: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
} 