using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class CommandWords
    {
        //create a dictory to hold the commands
        Dictionary<string, Command> commands;
        private static Command[] commandArray = { new GoCommand(), new QuitCommand(), new StartCommand(), new NameCommand(),
            new ExitCommand(), new PickUpCommand(), new InventoryCommand(),new DropCommand(), new OpenCommand(), new BuyCommand(), 
            new TrainCommand(), new BackCommand(), new EatCommand()};


        
        public CommandWords() : this(commandArray)
        {
        }

        //create a list of commands for guiding the user on what the available commands are
        public CommandWords(Command[] commandList)
        {
            commands = new Dictionary<string, Command>();
            foreach (Command command in commandList)
            {
                commands[command.name] = command;
            }
            Command help = new HelpCommand(this);
            commands[help.name] = help;
        }

        //returns the command after it searches for it in the dictionary
        public Command get(string word)
        {
            Command command = null;
            commands.TryGetValue(word, out command);
            return command;
        }

        //this prints out the current available commands
        public string description()
        {
            string commandNames = "";
            Dictionary<string, Command>.KeyCollection keys = commands.Keys;
            foreach (string commandName in keys)
            {
                commandNames += " " + commandName;
            }
            return commandNames;


        }
    }
}
