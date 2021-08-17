using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
  
        public class BackCommand : Command
        {//this method returns the previous rooms
            public BackCommand()
            {
                name = "back";
            }
            override
            public bool execute(Player player)
            {
                if (!this.hasSecondWord())
                {
                    player.back();
                }
                else
                {
                    player.warningMessage("\nBack " + secondWord + "???");
                }
                return false;
            }
        }
    
}
