using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    public class Dice
    {
        public int DiceID { get; set; }
        public int DiceValue { get; set; }
        public bool IsDiceEnabled { get; set; }
    }
}
