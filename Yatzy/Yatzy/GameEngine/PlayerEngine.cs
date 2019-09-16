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
        Player activePlayer;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private ObservableCollection<Player> _activePlayers;

        DBOps.DbOperations dbops;

        public ObservableCollection<Player> ActivePlayers
        {
            get { return _activePlayers; }
            set { _activePlayers = value; PropertyChanged(this, new PropertyChangedEventArgs("Players"));}
        }

        public PlayerEngine()
        {
            ActivePlayers = new ObservableCollection<Player>();
            activePlayer = new Player();
            
            AddPlayerHardCoded();
            SetActivePlayer();
        }

        public void AddPlayer(string firstname, string lastname, string nickname)
        {
            Player p = new Player
            {
                Firstname = firstname,
                Lastname = lastname,
                Nickname = nickname
            }; ActivePlayers.Add(p);
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

        public Player GetActivePlayer()
        {
            return activePlayer;
        }

        public ObservableCollection<Player> GetList()
        {
            return ActivePlayers;
        }

        public void SetActivePlayer()
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
        }


    }
}
