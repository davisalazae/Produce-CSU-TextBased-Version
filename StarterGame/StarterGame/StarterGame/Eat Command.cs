using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    public class EatCommand : Command
    {
        //this method  sets the name of the command equal to eat, so Command sees this and knows what to associate Eat() with
        public EatCommand() : base()
        {
            this.name = "eat";
        }

        override

            //if there is second word, call the method from player, if not return a warning
        public bool execute(Player player)
        {
            if (this.hasSecondWord())
            {
                player.eat(this.secondWord);
            }
            else
            {
                player.warningMessage("\neat what???");
            }
            return false;
        }
    }
}
