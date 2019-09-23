using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class Game : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;


        public int GameId { get; set; }
        public int HighestScore { get; set; }

        public int Player_Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NickName { get; set; }

    }
}
