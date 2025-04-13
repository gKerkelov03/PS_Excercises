using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DataLayer.Model;
using DataLayer.Services;
using UI.Commands;
using UI.Windows;
using DataLayer;

namespace UI.ViewModels
{
    public class DisciplineViewModel : ViewModelBase
    {
        private readonly IDisciplineService _disciplineService;
        private readonly Logger _logger;
        private readonly string _currentUsername;
        private ObservableCollection<Discipline> _disciplines;
        private Discipline _selectedDiscipline;
        private string _searchText = string.Empty;
        private string _statisticsText = string.Empty;

        public DisciplineViewModel(IDisciplineService disciplineService, Logger logger, string currentUsername)
        {
            _disciplineService = disciplineService;
            _logger = logger;
            _currentUsername = currentUsername;
            _disciplines = new ObservableCollection<Discipline>();
            
            AddDisciplineCommand = new AddDisciplineCommand(this);
            EditDisciplineCommand = new EditDisciplineCommand(this);
            DeleteDisciplineCommand = new DeleteDisciplineCommand(this);
            
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
                _logger.LogInfo($"Added new discipline: {discipline.Name}", _currentUsername);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding discipline: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogError($"Error adding discipline: {ex.Message}", _currentUsername);
            }
        }

        public async void ExecuteEditDiscipline(object parameter)
        {
            if (SelectedDiscipline == null) return;

            var disciplineToEdit = new Discipline
            {
                Id = SelectedDiscipline.Id,
                Name = SelectedDiscipline.Name,
                Description = SelectedDiscipline.Description,
                Year = SelectedDiscipline.Year,
                Semester = SelectedDiscipline.Semester,
                Credits = SelectedDiscipline.Credits,
                Lecturer = SelectedDiscipline.Lecturer,
                MaxStudents = SelectedDiscipline.MaxStudents
            };

            var editWindow = new DisciplineEditWindow(disciplineToEdit);
            if (editWindow.ShowDialog() == true)
            {
                try
                {
                    SelectedDiscipline.Name = disciplineToEdit.Name;
                    SelectedDiscipline.Description = disciplineToEdit.Description;
                    SelectedDiscipline.Year = disciplineToEdit.Year;
                    SelectedDiscipline.Semester = disciplineToEdit.Semester;
                    SelectedDiscipline.Credits = disciplineToEdit.Credits;
                    SelectedDiscipline.Lecturer = disciplineToEdit.Lecturer;
                    SelectedDiscipline.MaxStudents = disciplineToEdit.MaxStudents;

                    await _disciplineService.UpdateDisciplineAsync(SelectedDiscipline);
                    await LoadDisciplinesAsync();
                    _logger.LogInfo($"Updated discipline: {SelectedDiscipline.Name}", _currentUsername);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating discipline: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _logger.LogError($"Error updating discipline: {ex.Message}", _currentUsername);
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
                    var disciplineId = SelectedDiscipline.Id;
                    var disciplineName = SelectedDiscipline.Name;
                    await _disciplineService.DeleteDisciplineAsync(disciplineId);
                    await LoadDisciplinesAsync();
                    SelectedDiscipline = null;
                    _logger.LogInfo($"Deleted discipline: {disciplineName}", _currentUsername);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting discipline: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _logger.LogError($"Error deleting discipline: {ex.Message}", _currentUsername);
                }
            }
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
                _logger.LogError($"Error loading disciplines: {ex.Message}", _currentUsername);
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
                _logger.LogError($"Error updating statistics: {ex.Message}", _currentUsername);
            }
        }
    }
} 