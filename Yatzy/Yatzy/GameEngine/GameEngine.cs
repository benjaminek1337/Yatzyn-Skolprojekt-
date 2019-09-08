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

        public List<int> DicestestList = new List<int>(); //Testlista som ska tas bort efter att metoderna kopplats till rätt lista
        public int[] ArrayofCountedDices = new int[6];

        public void RollDiceTest() //Testmetod som ska tas bort
        {
            DicestestList.Add(3);
            DicestestList.Add(6);
            DicestestList.Add(2);
            DicestestList.Add(5);
            DicestestList.Add(4);

            DiceCount();

        }


        /// <summary>
        /// Konverterar tärningarna i en lista till en array med antal tärningar i varje kategori
        /// </summary>
        public void DiceCount()
        {
            for (int i = 0; i < 5; i++)
            {
                if (DicestestList[i] == 1)
                {
                    ArrayofCountedDices[0] += 1;
                }
                if (DicestestList[i] == 2)
                {
                    ArrayofCountedDices[1] += 1;
                }
                if (DicestestList[i] == 3)
                {
                    ArrayofCountedDices[2] += 1;
                }
                if (DicestestList[i] == 4)
                {
                    ArrayofCountedDices[3] += 1;
                }
                if (DicestestList[i] == 5)
                {
                    ArrayofCountedDices[4] += 1;
                }
                if (DicestestList[i] == 6)
                {
                    ArrayofCountedDices[5] += 1;
                }
            }
        }
        #region Publika metoder som hämtar poäng från varje kategori
        /// <summary>
        /// Räknar ut poängen för kategori 1-6
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Räknar ut poängen för det par som har högst värde
        /// </summary>
        /// <returns></returns>
        public int GetPair()
        {
            int sum = 0;
            for (int i = 5; i >= 0; i--)
            {
                if (ArrayofCountedDices[i] >= 2)
                {
                    sum = (i + 1) * 2;
                    break;
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för två par och kontrollerar att inte paren har samma värde
        /// </summary>
        /// <returns></returns>
        public int GetTwoPairs()
        {
            int sum = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (ArrayofCountedDices[j] >= 2 && ArrayofCountedDices[i] >= 2 && i != j)
                    {
                        sum = (i + 1 + j + 1) * 2;
                        break;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för tretal
        /// </summary>
        /// <returns></returns>
        public int GetThreeOfAKind()
        {
            int sum = 0;
            for (int i = 0; i < 6; i++)
            {
                if (ArrayofCountedDices[i] >= 3)
                {
                    sum = (i + 1) * 3;
                    break;
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för fyrtal
        /// </summary>
        /// <returns></returns>
        public int GetFourOfAKind()
        {
            int sum = 0;
            for (int i = 0; i < 6; i++)
            {
                if (ArrayofCountedDices[i] >= 4)
                {
                    sum = (i + 1) * 4;
                    break;
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för liten stege (Kombinationen 1,2,3,4,5)
        /// </summary>
        /// <returns></returns>
        public int GetSmallLadder()
        {
            int sum = 0;
            for (int i = 0; i < 5; i++)
            {
                if (ArrayofCountedDices[i] == 1)
                {
                    sum += i + 1;
                }
            }
            if (sum == 15)
            {
                return sum;
            }
            return 0;
        }
        /// <summary>
        /// Räknar ut poängen för stor stege (Kombinationen 2,3,4,5,6)
        /// </summary>
        /// <returns></returns>
        public int GetLargeLadder()
        {
            int sum = 0;
            for (int i = 1; i < 6; i++)
            {
                if (ArrayofCountedDices[i] == 1)
                {
                    sum += i + 1;
                }
            }
            if (sum == 20)
            {
                return sum;
            }
            return 0;
        }
        /// <summary>
        /// Räknar ut poängen för Kåk (Ett par och ett tretal)
        /// </summary>
        /// <returns></returns>
        public int GetFullHouse()
        {
            int sum = 0;

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    if (ArrayofCountedDices[i] == 2 && ArrayofCountedDices[j] == 3)
                    {
                        sum = (i + 1) * 2 + (j + 1) * 3;
                        break;
                    }
                }
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för Chans (Alla 5 tärningarna räknas ihop oavsett värde)
        /// </summary>
        /// <returns></returns>
        public int GetChance()
        {
            int sum = 0;

            for (int i = 0; i < DicestestList.Count; i++)
            {
                sum += DicestestList[i];
            }
            return sum;
        }
        /// <summary>
        /// Räknar ut poängen för Yatzy (Alla tärningar har samma värde)
        /// </summary>
        /// <returns></returns>
        public int GetYatzy() 
        {
            int sum = 0;
            for (int i = 1; i < 5; i++)
            {
                if (DicestestList[i] != DicestestList[0]) //Kontrollerar om tärning 2-5 är olik tärning 1 och returnerar i så fall 0
                {
                    return sum;
                }
            }
            return 50; 
        }
        #endregion

        #region Publika metoder som sätter poäng för varje kategori
        /// <summary>
        /// Tilldelar aktiv spelare poäng i varje kategori
        /// </summary>
        /// <param name="category"></param>
        public void SetScore(int category)
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
                case 7:
                    activePlayer.Pair = GetPair();
                    break;
                case 8:
                    activePlayer.Sixes = GetTwoPairs();
                    break;
                case 9:
                    activePlayer.Sixes = GetThreeOfAKind();
                    break;
                case 10:
                    activePlayer.Sixes = GetFourOfAKind();
                    break;
                case 11:
                    activePlayer.Sixes = GetSmallLadder();
                    break;
                case 12:
                    activePlayer.Sixes = GetLargeLadder();
                    break;
                case 13:
                    activePlayer.Sixes = GetFullHouse();
                    break;
                case 14:
                    activePlayer.Sixes = GetChance();
                    break;
                case 15:
                    activePlayer.Sixes = GetYatzy();
                    break;

                default:
                    break;
            }
        }
        #endregion
    }
}
