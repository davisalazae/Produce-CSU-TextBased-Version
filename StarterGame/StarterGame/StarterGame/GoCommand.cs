using System.Collections;
using System.Collections.Generic;

namespace StarterGame
{
    public class GoCommand : Command
    {
        //Allows you to traverse through the world

        public GoCommand() : base()
        {
            this.name = "go";
        }

        override
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.waltTo(this.secondWord);
            }
             
            else
            {
                player.warningMessage("\nGo Where?");
              
            }
            return false;
        }
    }
}
