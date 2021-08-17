using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    class DropCommand : Command
    {
        //This drops items into the rooms
        public DropCommand()
        {
            this.name = "drop";
        }

        override

        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.drop(this.secondWord);
            }
            else
            {
                player.warningMessage("\nDrop what?");
            }
            return false;

        }
    }
}
