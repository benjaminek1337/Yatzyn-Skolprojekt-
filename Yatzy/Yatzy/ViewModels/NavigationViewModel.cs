using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yatzy.Commands;

namespace Yatzy.ViewModels
{
    class NavigationViewModel : INotifyPropertyChanged
    {
        #region Properties

        public ICommand SettingsCommand { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public ICommand LeaderBoardCommand { get; set; }
        public ICommand AddPlayerCommand { get; set; }

        private object selectedViewModel;

        #endregion

        #region Contructor
        public object SelectedViewModel{

            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }

        }

        public NavigationViewModel()
        {
            SettingsCommand = new RelayCommand(OpenSettings, CanExecuteMethod);
            MainMenuCommand = new RelayCommand(OpenMainMenu, CanExecuteMethod);
            LeaderBoardCommand = new RelayCommand(OpenLeaderBoard, CanExecuteMethod);
            AddPlayerCommand = new RelayCommand(OpenAddPlayer, CanExecuteMethod);
        }

        #endregion

        #region Methods

        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }

        private void OpenSettings(object parameter)
        {
            SelectedViewModel = new SettingsViewModel();
        }

        private void OpenMainMenu(object parameter)
        {
            SelectedViewModel = new MainMenuViewModel();
        }

        private void OpenLeaderBoard(object parameter)
        {
            SelectedViewModel = new LeaderBoardViewModel();
        }

        private void OpenAddPlayer(object parameter)
        {
            SelectedViewModel = new AddPlayerViewModel();
        }

        #endregion

        #region Event Handlers
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged !=null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
