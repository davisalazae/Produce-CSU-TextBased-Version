using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //this method is for training
    class TrainCommand : Command
    {
        public TrainCommand()
        {
            name = "train";
        }
        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.train(secondWord);
            }
            else
            {
                player.warningMessage("\nTrain what??");
            }
            return false;
        }
    }
}
