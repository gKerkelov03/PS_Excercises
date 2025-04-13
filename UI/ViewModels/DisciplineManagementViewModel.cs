using System.Windows;
using DataLayer.Services;
using UI.Commands;
using UI.Windows;

namespace UI.ViewModels
{
    public class DisciplineManagementViewModel : ViewModelBase
    {
        private readonly IDisciplineService _disciplineService;
        private readonly DisciplineViewModel _disciplineViewModel;

        public DisciplineManagementViewModel(IDisciplineService disciplineService)
        {
            _disciplineService = disciplineService;
            _disciplineViewModel = new DisciplineViewModel(disciplineService);
            
            AddDisciplineCommand = new AddDisciplineCommand(_disciplineViewModel);
            EditDisciplineCommand = new EditDisciplineCommand(_disciplineViewModel);
            DeleteDisciplineCommand = new DeleteDisciplineCommand(_disciplineViewModel);
            RefreshCommand = new RefreshCommand(_disciplineViewModel);
        }

        public DisciplineViewModel DisciplineViewModel => _disciplineViewModel;

        public AddDisciplineCommand AddDisciplineCommand { get; }
        public EditDisciplineCommand EditDisciplineCommand { get; }
        public DeleteDisciplineCommand DeleteDisciplineCommand { get; }
        public RefreshCommand RefreshCommand { get; }
    }
} 