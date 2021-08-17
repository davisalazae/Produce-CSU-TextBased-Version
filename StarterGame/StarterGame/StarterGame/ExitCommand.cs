using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class ExitCommand :Command
    {
        //exits current parser state if not normalstate
        public ExitCommand()
        {
            name = "exit";
        }
        override
        public bool execute(Player player)
        {
            if (!this.hasSecondWord())
            {
                player.exit();
            }
            else
            {
                player.warningMessage("\nExit " + secondWord + "???");
            }
            return false;
        }
    }
}
