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

        public List<int> DicestestList = new List<int>();

        public List<Player> Players { get; set; }

        public GameEngine()
        {
            Players = new List<Player>();
        }

        public void RollDiceTest()
        {
            DicestestList.Add(2);
            DicestestList.Add(2);
            DicestestList.Add(4);
            DicestestList.Add(4);
            DicestestList.Add(4);

        }

        public int GetPair()
        {
            int sum = 0;
            var result = DicestestList.Where(num => num == 4);
            sum = result.Count();
            return sum;
        }

        

        public int GetUpperScore(int category)
        {
            int sum = 0;

            for (int i = 0; i < DicestestList.Count; i++)
            {
                if (DicestestList[i] == category)
                {
                    sum += category;
                }
            }
            return sum;
        }

        public void SetUpperScore(int category)
        {
            switch (category)
            {
                case 1:
                    activePlayer.Ones = GetUpperScore(1);
                    break;
                case 2:
                    activePlayer.Twos = GetUpperScore(2);
                    break;
                case 3:
                    activePlayer.Threes = GetUpperScore(3);
                    break;
                case 4:
                    activePlayer.Fours = GetUpperScore(4);
                    break;
                case 5:
                    activePlayer.Fives = GetUpperScore(5);
                    break;
                case 6:
                    activePlayer.Sixes = GetUpperScore(6);
                    break;
                default:
                    break;

            }
        }

        public int CalculateThreeOfAKind(List<int> diceList)
        {
            int Sum = 0;

            bool ThreeOfAKind = false;

            for (int i = 1; i <= 6; i++)
            {
                int Count = 0;
                for (int j = 0; j < 5; j++)
                {
                    if (diceList[j] == i)
                        Count++;

                    if (Count > 2)
                        ThreeOfAKind = true;
                }
            }

            if (ThreeOfAKind)
            {
                for (int k = 0; k < 5; k++)
                {
                    Sum += diceList[k];
                }
            }

            return Sum;
        }

        public int GetYatzy()
        {
            int sum = 0;
            for (int i = 1; i < 5; i++)
            {
                if (DicestestList[i] != DicestestList[0])
                {
                    return sum;
                }
            }
            sum = 50;
            return sum;
        }

    }
}
