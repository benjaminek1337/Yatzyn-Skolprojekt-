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
        private ObservableCollection<Player> players;

        public ObservableCollection<Player> Players
        {
            get { return players; }
            set { players = value; PropertyChanged(this, new PropertyChangedEventArgs("Players"));}
        }


        public void AddPlayerHardCoded()
        {
            if (Players.Count <= 4)
            {
                Player p = new Player
                {
                    Firstname = "Benjamin",
                    Lastname = "Ek",
                    Nickname = "Galne_Gunnar1337"

                }; Players.Add(p);
                Player p2 = new Player
                {
                    Firstname = "Erik",
                    Lastname = "Öberg",
                    Nickname = "Examinator"
                }; Players.Add(p2);
            }
        }


        public void SetActivePlayer()
        {
            int index = 0;

            for (int i = 0; i < Players.Count; i++)
            {
                if (Players[i].Equals(activePlayer))
                {
                    index = i;
                }
            }
            index++;
            if (index.Equals(Players.Count))
            {
                index = 0;
            }

            activePlayer = Players[index];
        }


    }
}
