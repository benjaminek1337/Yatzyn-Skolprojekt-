using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class DicesViewModel
    {

        public List<Dice> dices;

        public DicesViewModel()
        {
            Dice dice;
            dices = new List<Dice>();
            for (int i = 0; i < 5; i++)
            {
                dice = new Dice
                {
                    DiceID = i + 1,
                    IsDiceEnabled = true
                }; dices.Add(dice);
            }
        }

        public void SaveDice(int diceButtonValue)
        {
            
            for (int i = 0; i < dices.Count; i++)
            {
                if (dices[i].DiceID == diceButtonValue)
                {
                    dices[i].IsDiceEnabled = false;
                }
            }
        }


        public void RollDices()
        {
            for (int i = 0; i < dices.Count; i++)
            {
                if (dices[i].IsDiceEnabled)
                {
                    Random random = new Random();
                    int rand = random.Next(1, 7);

                    dices[i].DiceValue = rand;
                }

            }
        }


    }
}
