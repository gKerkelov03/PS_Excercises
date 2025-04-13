using System.Collections.ObjectModel;
using DataLayer.Model;
using DataLayer.Services;
using UI.Commands;
using UI.Windows;
using DataLayer;

namespace UI.ViewModels
{
    public class DisciplineManagementViewModel : DisciplineViewModel
    {
        public DisciplineManagementViewModel(IDisciplineService disciplineService, Logger logger, string currentUsername)
            : base(disciplineService, logger, currentUsername)
        {
        }
    }
} 