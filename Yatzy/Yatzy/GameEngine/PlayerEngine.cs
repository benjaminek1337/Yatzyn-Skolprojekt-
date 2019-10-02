using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Models;
using Yatzy.DBOps;


namespace Yatzy.GameEngine
{
    class PlayerEngine : INotifyPropertyChanged
    {
        #region Lokala objekt och variabler
        public int gameType = 0;
        #endregion

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        #endregion

        #region Properties

        private ObservableCollection<Player> _activePlayers;
        public ObservableCollection<Player> ActivePlayers
        {
            get { return _activePlayers; }
            set { _activePlayers = value; PropertyChanged(this, new PropertyChangedEventArgs("ActivePlayers"));}
        }
        Player activePlayer { get; set; }

        #endregion

        #region Konstruktor
        public PlayerEngine()
        {
            ActivePlayers = new ObservableCollection<Player>();
            activePlayer = new Player();
            //DicesViewModel dicesViewModel = new DicesViewModel();
            //SetActivePlayer();
        }
        #endregion

        #region Metod för att nollställa klassens props
        public void NullProps()
        {
            activePlayer = null;
            ActivePlayers = null;
        }
        #endregion

        #region Metoder för att ta emot och skicka ut typ av spel
        public void GetGameType(int _gameType)
        {
            gameType = _gameType;
        }

        public int SetGameType()
        {
            return gameType;
        }
        #endregion

        #region Metoder för att hantera listan över spelare, samt sätta "nästa spelare"
        public ObservableCollection<Player> SetPlayers()
        {
            return ActivePlayers;
        }

        public Player SetActivePlayer()
        {
            int index = -1;

            for (int i = 0; i < ActivePlayers.Count; i++)
            {
                if (ActivePlayers[i].Equals(activePlayer))
                {
                    index = i;
                }
            }
            index++;
            if (index.Equals(ActivePlayers.Count))
            {
                index = 0;
            }

            activePlayer = ActivePlayers[index];

            return activePlayer;
        }

        #endregion
    }
}
