using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class Dices
    {
        public void RollDices()
        {
            List<int> Dices = new List<int>();

            for (int i = 0; i < 6; i++)
            {
                Random random = new Random();
                int rand = random.Next(0, 5);

                Dices.Add(rand);
            }
        }
    }
}
