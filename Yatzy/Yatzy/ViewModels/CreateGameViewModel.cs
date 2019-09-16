using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Commands;
using Yatzy.GameEngine;
using Yatzy.Models;

namespace Yatzy.ViewModels
{
    class CreateGameViewModel : INotifyPropertyChanged
    {
        DicesViewModel dicesViewModel;
        PlayerEngine playerEngine; //Kasta in eller ut de valda spelarna till listan ActivePlayers eller vad fan den nu heter inne i PlayerEngine

        #region Properties
        RelayCommand ClassicGameCommand;
        RelayCommand StyrdGameCommand;
        RelayCommand AddPlayerCommand;
        RelayCommand RemovePlayerCommand;
        RelayCommand StartGameCommand;

        private int gameType = 4; //Ändra denna till 4 för klassisk eller 5 för styrd.

        #endregion

        #region Contructor

        public CreateGameViewModel()
        {
            ClassicGameCommand = new RelayCommand(, CanExecute);
            StyrdGameCommand = new RelayCommand(, CanExecute);
            AddPlayerCommand = new RelayCommand(, CanExecute);
            RemovePlayerCommand = new RelayCommand(, CanExecute);
            StartGameCommand = new RelayCommand(StartGame, CanExecute);

            //Ta bort kommentarsträcken när metoderna är skapade
        }

        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }


        public void GetACtivePlayers()
        {
            playerEngine.GetActivePlayer();
        }


        public void RemovePlayer()
        {
            
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        private void StartGame(object parameter)
        {
            dicesViewModel = new DicesViewModel(gameType);
        }


    }
}
