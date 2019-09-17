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
        PlayerEngine playerEngine;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        #region Properties 
        public RelayCommand AddPlayerCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        private Player player;
        public Player Player
        {
            get { return player; }
            set { player = value; PropertyChanged(this, new PropertyChangedEventArgs("Player")); }
        }

        #endregion

        #region Konstruktor
        public AddPlayerViewModel()
        {
            AddPlayerCommand = new RelayCommand(AddPlayer, CanExecute);
            CancelCommand = new RelayCommand(Cancel, CanExecute);
        }
        #endregion

        #region Metoder för att lägga till spelare

        private void AddPlayer(object parameter)
        {
            playerEngine.AddPlayer(player);
            //Fixa bindings till fnamn, enamn o nickname i textboxar i view nånstans.
        }

        private void Cancel(object parameter)
        {
            //Metod för att återgå till tidigare
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }

        #endregion



    }
}
