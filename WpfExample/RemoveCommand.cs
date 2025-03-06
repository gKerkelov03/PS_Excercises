using System.Windows.Input;

namespace WpfExample;

public class RemoveCommand : ICommand
{
    public bool CanExecute(object parameter)
    {
        return true;
    }
    
    public void Execute(object parameter)
    {
        var nameList = parameter as NamesList;
        var oldName = nameList.SelectedName;
        nameList.Names.Remove(oldName);
    }

    public event EventHandler? CanExecuteChanged;
}