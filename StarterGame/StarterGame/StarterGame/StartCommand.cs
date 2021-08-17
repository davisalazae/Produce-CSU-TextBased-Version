using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class StartCommand:Command 
    { //this alters the state of which the parser is in
        public StartCommand()
        {
            this.name = "start";
        }

        override
        public bool execute(Player player)
        {
            if(this.hasSecondWord())
            {
                player.start(this.secondWord);
            }
            else
            {
                player.warningMessage("\nStart what?");
            }
            return false;
        }
    }
}
