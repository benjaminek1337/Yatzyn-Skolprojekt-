using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using Yatzy.Commands;
using Yatzy.Models;
using System.Media;

namespace Yatzy.ViewModels
{
    class NavigationViewModel : INotifyPropertyChanged
    {

        SoundPlayer sPlayer;

        #region Properties

        public ICommand SettingsCommand { get; set; }
        public ICommand MainMenuCommand { get; set; }
        public ICommand LeaderBoardCommand { get; set; }
        public ICommand RulesCommand { get; set; }
        public ICommand CreateGamesCommand { get; set; }

        private object selectedViewModel;

        #endregion

        #region Contructor
        public object SelectedViewModel
        {
            get { return selectedViewModel; }
            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }
        }

        public NavigationViewModel()
        {
            SettingsCommand = new RelayCommand(OpenSettings, CanExecuteMethod);
            MainMenuCommand = new RelayCommand(OpenMainMenu, CanExecuteMethod);
            LeaderBoardCommand = new RelayCommand(OpenLeaderBoard, CanExecuteMethod);
            RulesCommand = new RelayCommand(OpenRules, CanExecuteMethod);
            CreateGamesCommand = new RelayCommand(OpenGameMenu ,CanExecuteMethod);
            //StartMainMusic();

        }

        #endregion

        #region Ljud
        private void StartMainMusic()
        {
            


            //MediaPlayer mp = new MediaPlayer();
            //mp.Open(new Uri("pack://application:,,,/otherAssemblyName;component/Sounds/Mainmenu.wav"));
            //mp.Play();
            sPlayer = new SoundPlayer();
            sPlayer.Stream = Properties.Resources.Main_menu;
            sPlayer.PlayLooping();
        }

        public void EndMainMusic ()
        {
            sPlayer.Stop();
        }


        #endregion


        #region Methods

        private bool CanExecuteMethod(object parameter)
        {
            return true;
        }

        private void OpenGameMenu(object parameter)
        {            
            SelectedViewModel = new CreateGameViewModel();
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

        private void OpenRules(object parameter)
        {
            SelectedViewModel = new NavigationViewModel();
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
