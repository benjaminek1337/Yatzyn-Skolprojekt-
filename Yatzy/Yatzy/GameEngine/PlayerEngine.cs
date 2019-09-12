using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Models;

namespace Yatzy.GameEngine
{
    class PlayerEngine : INotifyPropertyChanged
    {
        Player activePlayer;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private ObservableCollection<Player> _activePlayers;

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

        public void AddPlayerHardCoded()
        {
            //if (ActivePlayers.Count <= 4)
            //{
                Player p = new Player
                {
                    Firstname = "Benjamin",
                    Lastname = "Ek",
                    Nickname = "Galne_Gunnar1337"

                }; ActivePlayers.Add(p);
                Player p2 = new Player
                {
                    Firstname = "Erik",
                    Lastname = "Öberg",
                    Nickname = "Examinat0rN^"
                }; ActivePlayers.Add(p2);
            //}
        }

        public Player GetActivePlayer()
        {
            return activePlayer;
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
