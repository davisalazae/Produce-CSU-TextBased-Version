using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class OpenCommand : Command
    {
        //allows youu to open doors
        public OpenCommand()
        {
            this.name = "open";
        }
        override
         public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.open(this.secondWord);
            }
            else
            {
                player.warningMessage("\nOpen What?");
            }
            return false;
        }
    }
}
