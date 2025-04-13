using System.Windows.Controls;
using DataLayer.Database;
using Microsoft.EntityFrameworkCore;

namespace UI.Components;

public partial class StudentsList : UserControl
{
    public StudentsList()
    {
        InitializeComponent();

        var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
        var solutionFolderName = "PS_Excercises";
        var databaseFileName = "database.db";
        var documentsFolderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
        var solutionFolder = System.IO.Path.Combine(documentsFolderPath, solutionFolderName);
        var databasePath = System.IO.Path.Combine(solutionFolder, databaseFileName);
        optionsBuilder.UseSqlite($"Data Source={databasePath}");
        
        using var context = new DatabaseContext(optionsBuilder.Options);
        var records = context.Users.ToList();
        students.DataContext = records;
    }
}