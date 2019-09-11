using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yatzy.Commands;
using Yatzy.GameEngine;
using Yatzy.Models;

namespace Yatzy.ViewModels
{
    class AddPlayerViewModel : INotifyPropertyChanged
    {
        PlayerEngine playerengine;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Properties 
        public RelayCommand SavePlayerCommand { get; set; }
        public RelayCommand AddPlayerCommand { get; set; }

        private ObservableCollection<Player> _players;
        public ObservableCollection<Player> Players
        {
            get { return _players; }
            set
            {
                _players = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Players"));
            }
        }

        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; PropertyChanged(this, new PropertyChangedEventArgs("Player")); }
        }

        #endregion

















        public void AddPlayer(string player)
        {
            if (Players.Count <= 4)
            {
                Player p = new Player
                {
                    Firstname = "",
                    Lastname = "",
                    Nickname = ""

                }; Players.Add(p);
            }
        }

    }
}
