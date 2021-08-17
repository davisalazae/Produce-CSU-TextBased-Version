using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //exits the program

    public class NameCommand : Command
    {
        public NameCommand()
        {
            this.name = "name";
        }

        override

        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.name(this.secondWord);
                
            }
            else
            {
                player.warningMessage("\nWhat name?");
            }
            return false;
        }
    }
}
