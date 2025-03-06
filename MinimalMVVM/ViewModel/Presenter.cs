using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MinimalMVVM.Model;

namespace MinimalMVVM.ViewModel
{
    public class Presenter : ObservableObject
    {
        private readonly TextConverter _textConverterToUpper = new TextConverter(s => s.ToUpper());
        private readonly TextConverter _textConverterToLower = new TextConverter(s => s.ToLower());
        private string _someText;
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();

        public string SomeText
        {
            get { return _someText; }
            set
            {
                _someText = value;
                RaisePropertyChangedEvent("SomeText");
            }
        }

        public IEnumerable<string> History
        {
            get { return _history; }
        }

        public ICommand ConvertTextToUpperCommand
        {
            get { return new DelegateCommand(ConvertTextToUpper); }
        }

        public ICommand ConvertTextToLowerCommand
        {
            get { return new DelegateCommand(ConvertTextToLower); }
        }
        
        private void ConvertTextToUpper()
        {
            AddToHistory(_textConverterToUpper.ConvertText(SomeText));
            SomeText = String.Empty;
        }

        private void ConvertTextToLower()
        {
            AddToHistory(_textConverterToLower.ConvertText(SomeText));
            SomeText = String.Empty;
        }
        
        private void AddToHistory(string item)
        {
            if (!_history.Contains(item))
                _history.Add(item);
        }
    }
}
