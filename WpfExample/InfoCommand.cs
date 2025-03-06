using System.Windows;
using System.Windows.Input;

namespace WpfExample;

public class InfoCommand : ICommand
{
    public bool CanExecute(object? parameter)
    {
        return true;
    }

    public void Execute(object? parameter)
    { 
        MessageBox.Show("Hello, world!");;
        var window = new NamesWindow();
        
        window.ShowDialog(); 
    }

    public event EventHandler? CanExecuteChanged;
}