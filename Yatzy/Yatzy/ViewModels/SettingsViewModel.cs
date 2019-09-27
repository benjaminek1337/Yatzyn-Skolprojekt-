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
using System.Windows.Controls;

namespace Yatzy.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        #region Properties

        public MediaPlayer mediaPlayer { get; set; }


        public ICommand ShowPPMCommand { get; set; }
        private bool _showppm;
        public bool ShowPPM
        {
            get { return _showppm; }
            set { _showppm = value; OnPropertyChanged("ShowPPM"); }
        }

        #endregion

        public SettingsViewModel()
        {
            ShowPPMCommand = new RelayCommand(TogglePPM, CanExecuteMethod);
            ShowPPM = false;
        }
        #region Methods
        public void ToggleSound()
        {
           
        }

        public void TogglePPM(object paramater)
        {
            ShowPPM = true;
        }

        private bool CanExecuteMethod(object parameter)
        {
            return true;
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
    }
}
