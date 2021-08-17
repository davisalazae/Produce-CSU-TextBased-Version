using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //allows player to pick up items
   public class PickUpCommand : Command
    {
        public PickUpCommand()
        {
            this.name = "pickup";
        }

        override

        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.pickup(this.secondWord);
            }
            else
            {
                player.warningMessage("\nPick up what?");
            }
            return false;

        }

    }
}
