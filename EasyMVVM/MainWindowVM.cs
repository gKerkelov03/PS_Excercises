using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace EasyMVVM;

public class MainWindowVM : DependencyObject, INotifyPropertyChanged
{
    private ObservableCollection<string> _BackingProperty;
    
    public ObservableCollection<string> BoundProperty
    {
        get { return _BackingProperty; }
        set { _BackingProperty = value; PropChanged("BoundProperty"); }
    }
    
    ObservableCollection<string> _data = new ObservableCollection<string>(); 
    public event PropertyChangedEventHandler? PropertyChanged;
    
    public void PropChanged(String propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    
    public ObservableCollection<string> GetData()
    { 
        _data.Add("First Entry");
        _data.Add("Second Entry");
        _data.Add("Thrd Entry");
        return _data;
    }

    

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public MainWindowVM()
    {
        Model m = new Model();
        BoundProperty = m.GetData();
    }
}