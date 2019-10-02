using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;
using System.Windows.Media;
using System.Windows;
using System.Windows.Input;
using Yatzy.Commands;

namespace Yatzy.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        #region Properties

        public MediaPlayer MediaPlayer { get; set; }
        public ICommand BackCommand { get; set; }


        #endregion

        #region Contstructor
        public SettingsViewModel()
        {
            BackCommand = new RelayCommand(Backcommand, CanExecuteMethod);            
        }
        #endregion

        #region Event Handlers
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods for going back
        private object selectedViewModel;
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }
        public void Backcommand(object parameter)
        {
            SelectedViewModel = new MainMenuViewModel();
        }
        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }
        #endregion
    }
}
