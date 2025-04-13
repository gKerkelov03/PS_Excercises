using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DataLayer.Model;
using DataLayer.Services;
using UI.Commands;

namespace UI.ViewModels
{
    public class DisciplineEditViewModel : ViewModelBase
    {
        private readonly Discipline _discipline;
        private readonly IDisciplineService _disciplineService;
        private readonly Window _window;

        public DisciplineEditViewModel(Discipline discipline, IDisciplineService disciplineService, Window window)
        {
            _discipline = discipline;
            _disciplineService = disciplineService;
            _window = window;

            SaveCommand = new SaveDisciplineCommand(this);
            CancelCommand = new CancelCommand(this);

            // Initialize collections
            Years = new List<int> { 1, 2, 3, 4 };
            Semesters = new List<int> { 1, 2 };
        }

        public string Name
        {
            get => _discipline.Name;
            set
            {
                _discipline.Name = value;
                OnPropertyChanged();
            }
        }

        public int Year
        {
            get => _discipline.Year;
            set
            {
                _discipline.Year = value;
                OnPropertyChanged();
            }
        }

        public int Semester
        {
            get => _discipline.Semester;
            set
            {
                _discipline.Semester = value;
                OnPropertyChanged();
            }
        }

        public int Credits
        {
            get => _discipline.Credits;
            set
            {
                _discipline.Credits = value;
                OnPropertyChanged();
            }
        }

        public string? Lecturer
        {
            get => _discipline.Lecturer;
            set
            {
                _discipline.Lecturer = value;
                OnPropertyChanged();
            }
        }

        public string? Description
        {
            get => _discipline.Description;
            set
            {
                _discipline.Description = value;
                OnPropertyChanged();
            }
        }

        public List<int> Years { get; }
        public List<int> Semesters { get; }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public async Task SaveAsync()
        {
            await _disciplineService.UpdateDisciplineAsync(_discipline);
            _window.DialogResult = true;
            _window.Close();
        }

        public void Cancel()
        {
            _window.DialogResult = false;
            _window.Close();
        }
    }
} 