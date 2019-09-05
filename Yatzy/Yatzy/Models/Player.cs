using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class Player
    {
        #region player properties
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Nickname { get; set; }
        #endregion
        #region dice results
        public int Ones { get; set; }
        public int Twos { get; set; }
        public int Threes { get; set; }
        public int Fours { get; set; }
        public int Fives { get; set; }
        public int Sixes { get; set; }
        public int Sum { get; set; }
        public int Bonus { get; set; }

        public int Pair { get; set; }
        public int TwoPairs { get; set; }
        public int ThreeOfaKind { get; set; }
        public int FourOfaKind { get; set; }
        public int SmalLadder { get; set; }
        public int LargeLadder { get; set; }
        public int FullHouse { get; set; }
        public int Chance { get; set; }
        public int Yatzy { get; set; }
        #endregion
        public int Total { get; set; }

        public int TotalUpper
        {
            get
            {
                return Ones + Twos + Threes + Fours + Fives + Sixes;
            }
        }


    }
}
