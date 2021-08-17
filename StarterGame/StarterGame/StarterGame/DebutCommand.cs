using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class DebutCommand : Command
    {
        //allows the player to debut and determine whether they win
        public DebutCommand()
        {
            this.name = "debut";
        }

        override
        public bool execute(Player player)
        {
            if (!this.hasSecondWord())
            {
                player.debut();
            }
            else
            {
                player.warningMessage("\nDebut "+secondWord+" ?");
            }
            return false;
        }
    }
}
