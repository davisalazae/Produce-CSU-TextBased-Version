using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //This a command class for the command "buy"
   public class BuyCommand : Command
    {
        public BuyCommand() : base()
        {
            this.name = "buy";
        }

        override

        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.buy(this.secondWord);
            }
            else
            {
                player.warningMessage("\nBuy h???");
            }
            return false;
        }
    }
}
