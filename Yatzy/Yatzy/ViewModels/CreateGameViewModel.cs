using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Commands;
using Yatzy.GameEngine;
using Yatzy.Models;

namespace Yatzy.ViewModels
{
    class CreateGameViewModel
    {
        DicesViewModel dicesViewModel;
        PlayerEngine playerEngine; //Kasta in eller ut de valda spelarna till listan ActivePlayers eller vad fan den nu heter inne i PlayerEngine

        RelayCommand ClassicGameCommand;
        RelayCommand StyrdGameCommand;
        RelayCommand AddPlayerCommand;
        RelayCommand RemovePlayerCommand;
        RelayCommand StartGameCommand;

        private int gameType = 4; //Ändra denna till 4 för klassisk eller 5 för styrd.

        public CreateGameViewModel()
        {
            //ClassicGameCommand = new RelayCommand();
            //StyrdGameCommand = new RelayCommand();
            //AddPlayerCommand = new RelayCommand();
            //RemovePlayerCommand = new RelayCommand();
            //StartGameCommand = new RelayCommand();

            //Ta bort kommentarsträcken när metoderna är skapade
        }

        private void StartGame()
        {
            dicesViewModel = new DicesViewModel(gameType);
        }


    }
}
