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
        #region Objekt och variabler
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

        #region Construktor
        public PlayerEngine()
        {
            ActivePlayers = new ObservableCollection<Player>();
            activePlayer = new Player();
            //DicesViewModel dicesViewModel = new DicesViewModel();
            //SetActivePlayer();
        }
        #endregion

        #region Metod för att nulla Props
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

        public void AddPlayerHardCoded()
        {
            Player p = new Player
            {
                Firstname = "Beendjaameeehn",
                Lastname = "Ek",
                Nickname = "Galne_Gunnar1337"
            }; ActivePlayers.Add(p);
            //Player p2 = new Player
            //{
            //    Firstname = "Näääädaaliiiiee",
            //    Lastname = "Öberg",
            //    Nickname = "Examinat0rN^"
            //}; ActivePlayers.Add(p2);
            //Player p3 = new Player
            //{
            //    Firstname = "Däääämn",
            //    Lastname = "Öberg",
            //    Nickname = "Examinat0rN^"
            //}; ActivePlayers.Add(p3);
        }

        //public Player GetActivePlayer()
        //{
        //    return activePlayer;
        //}

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
