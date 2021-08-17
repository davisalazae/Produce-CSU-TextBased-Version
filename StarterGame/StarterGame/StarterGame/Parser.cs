using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    //states the rooms are in are created here
    public enum ParserState { Normal, Train, Debut, Buy, Character }

   //create an interface that gets the state and enters it when player desires
    public interface IParserState
    {
        ParserState State { get; }

        void Enter(Parser parser);
        void Exit(Parser parser);

    }

    //for the normalstate, nothing changes and all the commands are present
    public class ParserNormalState : IParserState
    {
        public ParserState State { get { return ParserState.Normal; } }
        public ParserNormalState()
        {

        }
        public void Enter(Parser parser)
        {

        }
        public void Exit(Parser parser)
        {

        }
    }

    //in the train state, the only commands that will be usable are: train, exit, and quit
    public class ParserTrainState : IParserState
    {
        public ParserState State { get { return ParserState.Train; } }
        public ParserTrainState()
        {

        }

        public void Enter(Parser parser)
        {
            Command[] commandArray = { new QuitCommand(), new TrainCommand(), new ExitCommand() };
            parser.Push(new CommandWords(commandArray));
        }

        //when you wish to exit, parser will pop the state off and return back to a normal state
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }

   // in the buy state, the only commands that will be usuable are: buy, exit, and quit
    public class ParserBuyState : IParserState
    {
        public ParserState State { get { return ParserState.Buy; } }
        public ParserBuyState()
        {

        }

        public void Enter(Parser parser)
        {
            Command[] commandArray = { new QuitCommand(), new ExitCommand(), new BuyCommand() };
            parser.Push(new CommandWords(commandArray));
        }

        //returns the state back to normal
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }

    // in the character state, the only commands that will be usuable are: name, exit, and quit
    public class ParserCharacterState : IParserState
    {
        public ParserState State { get { return ParserState.Character; } }
        public ParserCharacterState()
        {

        }

        public void Enter(Parser parser)
        {
            Command[] commandArray = { new QuitCommand(), new NameCommand(), new ExitCommand() };
            parser.Push(new CommandWords(commandArray));
        }
        //returns to normal state
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }

    // in the debut state, the only commands that will be usuable are: debut, exit, and quit
    public class ParserDebutState : IParserState
    {
        public ParserState State { get { return ParserState.Debut; } }
        public ParserDebutState()
        {

        }

        public void Enter(Parser parser)
        {

            Command[] commandArray = { new QuitCommand(), new DebutCommand(), new ExitCommand() };
            parser.Push(new CommandWords(commandArray));

        }
        public void Exit(Parser parser)
        {
            parser.Pop();
        }
    }


    public class Parser
    {
        //create a stack for commands to be received by the cpu
        private Stack<CommandWords> commands;

        //create an instance variable called _State that will let the parser know which state the player is in and declare it
        private IParserState _state;
        public IParserState State
        {
            get
            {
                return _state;
            }

            set
            {
                _state.Exit(this);
                _state = value;
                _state.Enter(this);
            }
        }
        public Parser() : this(new CommandWords())
        {

        }

        //this reads in the command for the parser to know which state the player is in 
        public Parser(CommandWords newCommands)
        {
            commands = new Stack<CommandWords>();
            _state = new ParserNormalState();

            //commands = newCommands;
            Push(newCommands);
            NotificationCenter.Instance.addObserver("PlayerWillEnterState", PlayerWillEnterState);

        }

        //notifies that the player has entered a state
        public void PlayerWillEnterState(Notification notification)
        {
            Player player = (Player)notification.Object;
            Dictionary<string, Object> userInfo = notification.userInfo;
            IParserState state = (IParserState)userInfo["state"];
            if (State.State == ParserState.Character)
            {
                player.currentRoom = GameWorld.Instance.Entrance;
            }

           /* if (State.State == ParserState.Buy)
            {
                player.currentRoom = GameWorld.Instance.Trigger;
            }

            if (State.State == ParserState.Train)
            {
                player.currentRoom = GameWorld.Instance.TrainingRoom1;
                player.currentRoom = GameWorld.Instance.TrainingRoom2;
                player.currentRoom = GameWorld.Instance.TrainingRoom3;
                player.currentRoom = GameWorld.Instance.TrainingRoom4;
            }
            if (State.State == ParserState.Debut)
            {
                player.currentRoom = GameWorld.Instance.DebutRoom;
            } */
            
            State = state;
        }

        //pushes commands into the stack
        public void Push(CommandWords newCommands)
        {
            commands.Push(newCommands);
        }

        //returns the first one on the top
        public void Pop()
        {
            commands.Pop();
        }

        //allows for the user to have multiple worded commands
        public Command parseCommand(string commandString)
        {
            Command command = null;
            string[] words = commandString.Split(' ');
            if (words.Length > 0)
            {
                command = commands.Peek().get(words[0]);
                if (command != null)
                {
                    if (words.Length > 1)
                    {
                        command.secondWord = words[1];

                    }
                    else
                    {
                        command.secondWord = null;

                    }
                }
                else
                {
                    Console.WriteLine(">>>Did not find the command " + words[0]);
                }
            }
            else
            {
                Console.WriteLine("No words parsed!");
            }
            return command;
        }

        public string description()
        {
            return commands.Peek().description();
        }
    }
}
