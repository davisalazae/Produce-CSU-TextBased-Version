using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //this gives all the items currently in the users possession
    public class InventoryCommand : Command
    { 
        public InventoryCommand() : base()
        {
            this.name = "inventory";
        }
        override 
        
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.warningMessage("\nI cannot inventory " + secondWord);
            }
            else
            {
                player.showInventory();
            }
            return false;
        }

   }
}
