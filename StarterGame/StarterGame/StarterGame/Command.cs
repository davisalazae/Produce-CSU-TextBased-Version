using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public abstract class Command
    {
        //command is primarly for setting a base foundation for the name, whether the command should have a second input, and then it executes
        //the command if applicable
        private string _name;
        public string name { get { return _name; } set { _name = value; } }
        private string _secondWord;
        
        public string secondWord { get { return _secondWord; } set { _secondWord = value; } }

        public Command()
        {
            this.name = "";
            this.secondWord = null;
           
        }

        public bool hasSecondWord()
        {
            return this.secondWord != null;
        }

        public abstract bool execute(Player player);
    }
}
