using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy.Models
{
    class DicesViewModel
    {
        public int DiceID { get; set; }
        public int Dice { get; set; }
        public bool IsDiceActive { get; set; }


        public List<DicesViewModel> dices;

        public DicesViewModel()
        {
            dices = new List<DicesViewModel>();
            for (int i = 0; i < 5; i++)
            {
                dices[i].DiceID = i + 1;
                dices[i].IsDiceActive = true;
            }
        }

        public void SaveDice(List<DicesViewModel> dices, int diceButtonValue)
        {
            
            for (int i = 0; i < dices.Count; i++)
            {
                if (DiceID == diceButtonValue)
                {
                    IsDiceActive = false;
                }
            }
        }


        public void RollDices(List<DicesViewModel> dices)
        {
            for (int i = 0; i < dices.Count; i++)
            {
                if (IsDiceActive)
                {
                    Random random = new Random();
                    int rand = random.Next(1, 7);

                    dices[i].Dice = rand;
                }

            }
        }


    }
}
