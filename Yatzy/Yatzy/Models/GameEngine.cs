using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class GameEngine
    {
        Player activePlayer;

        public List<Player> Players { get; set; }

        public GameEngine()
        {
            Players = new List<Player>();
        }

        
    }
}
