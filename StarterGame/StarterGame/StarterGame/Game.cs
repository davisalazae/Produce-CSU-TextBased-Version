using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Game
    {
        
        Player player;
        Parser parser;
        bool playing;
        Queue<Command> commandQueue;
        public Game()
        {
            playing = false;
            parser = new Parser(new CommandWords());
            player = new Player(GameWorld.Instance.CharacterCreate);
            
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            userInfo["state"] = new ParserCharacterState();
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", player, userInfo));
            commandQueue = new Queue<Command>();
            
        }

        public void PlayerWillEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            player.debugMessage("The player is now in " + player.currentRoom.tag);
       
        }

        /**
     *  Main play routine.  Loops until end of play.
     */
        public void play()
        {

            // Enter the main command loop.  Here we repeatedly read commands and
            // execute them until the game is over.
            
            bool finished = false;
            while (!finished)
            {
                Console.Write("\n"+ player.Name + ">");
                Command command = parser.parseCommand(Console.ReadLine());
                if (command == null)
                {
                    player.warningMessage("I don't understand...");
                }
                else
                {
                    finished = command.execute(player);
                }
            }
        }


        public void start()
        {
            playing = true;
            player.outputMessage(welcome());
            processCommandQueue();
        }

        public void end()
        {
            playing = false;
            player.outputMessage(goodbye());
        }

        public string welcome()
        {
           return "Welcome to Produce CSU!\n Do you think you have what it takes to make it as an idol?.In this game, your drive triggers your rate of success" +
                "Will you finally rise to stardom or fail and never have your dreams come true?\n\nType 'help' if you need help." + player.currentRoom.description();
        }

        public string goodbye()
        {
            return "\nThank you for playing, Goodbye. \n";
        }

        public void processCommandQueue()
        {
            while(commandQueue.Count > 0)
            {
                Command command = commandQueue.Dequeue();
                player.outputMessage(">" + command);
                command.execute(player);

            }
            
        }
    }
}
