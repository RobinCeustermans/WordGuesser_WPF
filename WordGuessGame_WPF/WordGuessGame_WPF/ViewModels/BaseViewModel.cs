using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WordGuessGame_DAL.Context;
using WordGuessGame_DAL.DataBaseOperation;

namespace WordGuessGame_WPF.ViewModels
{
    public abstract class BaseViewModel : IDataErrorInfo, INotifyPropertyChanged, ICommand
    {
        public GameDataManager GameManager { get; set; }

        protected BaseViewModel()
        {
            var context = new WordGuessGameEntities();
            GameManager = new GameDataManager(context);
        }

        public abstract string this[string columnName] { get; }
        public string Error
        {
            get
            {
                string errorMessages = "";
                foreach (var item in GetType().GetProperties()) //reflection 
                {
                    string error = this[item.Name];
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        errorMessages += error + Environment.NewLine;
                    }
                }
                return errorMessages;
            }
        }




        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public abstract bool CanExecute(object? parameter);

        public abstract void Execute(object? parameter);




        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //helpers
        public bool IsValid()
        {
            return string.IsNullOrWhiteSpace(Error);
        }
    }
}
