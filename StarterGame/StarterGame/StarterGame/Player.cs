
using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public class Player
    {
        private IItem edible;
        
        //Call for currentroom to know where player is
        private Room _currentRoom = null;
        public Room currentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                _currentRoom = value;
            }
        }
        private Room _previousRoom = null;

        public Room previousRoom
        {
            get
            {
                return _previousRoom;
            }
            set
            {
                _previousRoom = value;
            }
        }
        private SkillLevel _status;
        public SkillLevel status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }
        bool win = false;
       
        //Create a stack that holds all previous rooms
       private Stack<Room> visit = new Stack<Room>();

        private Room visitor;
        public Room visited
        {
            get
            {
                return visitor;
            }
            set
            {
               
                    visitor= value;
                    //visit.Push(visitor);
                 
            }
        } 
        private float _wallet;
        private IBodega product;
        //constructor for player which will have the players current location, their name, and inventory and skill level
        public Player(Room room)//, GameOutput output)
        {
            _currentRoom = room;
            currentRoom = room;
            _previousRoom = room;
            previousRoom = room;
            Name = "";
            inventory = new ItemContainer();
            level = new SkillLevel();
            visitor= new Room();
            visit = new Stack<Room>();
            product = new Bodega();
            _status = new SkillLevel();
            _wallet = 12f;
            edible = new Item();
            
        }
        public String Name { set; get; }

        //call methods 

        private IItemContainer inventory;

        private ISkillLevel level;

        //this method pops the previous room on the top of the stack for the user to return to
       public void back()
        {
           
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["state"] = new ParserNormalState();
            //send a notification that the user will enter a room
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterRoom", this));
            //pops the room
            _currentRoom = visit.Pop();


            //send a notification that the user did enter a room
           NotificationCenter.Instance.postNotification(new Notification("PlayerDidEnterRoom", this));
            this.informationMessage("\n" + this._currentRoom.description());   
        }
        
        //This method is apart of the buy command. It allows the user to purchase items if they have funds
       public void buy(string item)
        {
            //have a wallet with a max amount to reduce from
            float wallet = _wallet;
            
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillBuyItem", this));
            //Switch the state to a buystate
            userInfo["state"] = new ParserBuyState();
            userInfo["word"] = item;
            if (wallet != 0f)
            { 
                IBodega product = (Bodega)currentRoom.pickup(item);
                charge(item, product.BuyValue);
                give(product);
           
               
            }
            else
            {
                warningMessage("You don't have enough money");
            }
            NotificationCenter.Instance.postNotification(new Notification("PlayerDidBuyItem", this, userInfo));
            informationMessage(">>>" + item + "\n");
        }
        
        //this method applies charges to wallet
        public void charge(string item, float amount)
        {
            
            amount = product.BuyValue;
            float twallet = _wallet;

            Dictionary<string, Object> userInfo = new Dictionary<string, object>();
            //product = take(item);
            if (item != null)
            {

                twallet = twallet - amount;
                pickup(item);
                informationMessage("\n The item " + item + " is now in your inventory");
                informationMessage("\nYou have $" + twallet + " left.");
            }
            else if (_wallet == 0f)
            {
                warningMessage("You don't have enough for " + item);
            }

        }

       //this method checks to see if the skills are suitable for debutting
        public void debut()
        {
          
            //if the skill levels are 7+, the debut is successful
            if (level.DanceSkill >= 7f && level.SingingSkill >= 7f && level.ModelingSkill >= 7f && level.GroupLevel >= 7f)
            {
                CongratsMessage("Congratulations! Your group blew away the Ceo and he can't wait to make money- I mean see your group prosper.");
                CongratsMessage("\n Be prepared to hear a sea of fans scream your name. Your hardwork won't go unnoticed.");
                
                
            }
            //if the skill is greater than or equal to 4 but less than 6, then they will disband shortly after debuting
            else if (level.DanceSkill == 5f && level.SingingSkill == 5f && level.ModelingSkill == 5f && level.GroupLevel == 5f)
            {
              
                CongratsMessage("Congratulations! Although your stage performance wasn't the best, your group was able to debut!");
                FailMessage("\nUnfortunately, your group disbanded a couple of months after you released your first single. Back to training.");
            }

            //else they fail
            else
            {
                
                FailMessage("I don't know whether it was the lack of harmonization or how unsynchronized your group was but, the ceo was not impressed.. ");
                FailMessage("Maybe dedicate more time into training next time");      
            }
            
        }

        //this allows you to drop items in any room
        public void drop(String itemName)
        {
            IItem item = take(itemName);
           
            if (item != null)
            {
                //if you have an item to drop into the room, drop it, if not return an error
                currentRoom.drop(item);
                informationMessage("\nthe item " + itemName + " is now in the room.");
            }
            else
            {
                warningMessage("\nThe item " + itemName + " is not in your inventory");
            }
        }

        //this method is for eating. To be able to determine what is is edible, it has to be a certain weight
        public void eat(string itemName)
        {
           
            if (itemName != null && edible.Weight <2.0f)
            {
                level.Energy += 1.0f;
                drop(itemName);
                informationMessage(level.ToString());
            }
            else
            {
                warningMessage("\n" + itemName + " appears to be unedible....");
            }

        }
   
        //this exits the current room that the player was stuck in
        public void exit()
        {
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["state"] = new ParserNormalState();
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
            this.informationMessage("\n" + this._currentRoom.description());
        }

        //this method allows you to give an item to another character
        public void give(IItem item)
        {
            inventory.put(item);
        }

        //this method allows you to name your yourself
        public void name(string newName)
        {
            Name = newName;
        }

        //this method allows you to unlock doors
        public void open(string direction)
        {
            Door door = this._currentRoom.getExit(direction);
            if (door != null)
            {
                if (door.State == OCState.Open)
                {
                    this.informationMessage("\nThe door on " + direction + " is already open");
                }
                else
                {
                    if (door.open().State == OCState.Open)
                    {
                        this.informationMessage("\nThe door on " + direction + " is now open");
                    }
                    else
                    {
                        this.warningMessage("\nThe door on" + direction + "did NOT open.");
                    }

                }
            }
            else
            {
                this.warningMessage("\nThere is no door on " + direction);
            }

        }

        //this allows you to pick up items in the room around you up to a certain weight
        public void pickup(string itemName)
        {
           
            if (edible.Weight <= 5f)
            {
                if (itemName != null)
                {

                    //if the item is in the room, obtain it,if not return an error
                    give(edible);
                    informationMessage("\n The item " + itemName + " is now in your inventory");

                }
                else
                {
                    warningMessage("\nThe item " + itemName + " does not exist in this room.");
                }
            }
            else
            {
                
              warningMessage("\nThis item is too heavy to carry");      
            }
        }

         //this shows what you currently have in your inventory
        public void showInventory()
        {
            informationMessage("\nInventory. \n" + inventory.contents());
        }

        //this method starts the beginning of a parserstate
        public void start(string state)
        {
            Dictionary<string, Object> userInfo = new Dictionary<string, object>();

            if (state.Equals("train"))
            {
                userInfo["state"] = new ParserTrainState();
            }
           
            else if (state.Equals("buy"))
            {
                userInfo["state"] = new ParserBuyState();
            }

            else if (state.Equals("debut"))
            {
                userInfo["state"] = new ParserDebutState();
            }


            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
        }

        //this allows you to take items from characters
        public IItem take(string itemName)
        {
            return inventory.remove(itemName);
        }

        //this method allows the user to train to increase their skills
        public void train(string train)
        {
            Dictionary<string, object> userInfo = new Dictionary<string, object>();
            userInfo["state"] = new ParserTrainState();
            NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterState", this, userInfo));
            this.informationMessage("\n Player will start training");
            informationMessage("\nItems that can be used to train with are: headphones, sheetmusic, and mirror. \n" );

            //If there is a level, give the training options
            if (train != null)
            {
                if (level.Energy != 0)
                {
                    //if individual, every skill will increase by a point except for group level. energy will decrease
                    if (train == "individual")
                    {
                        level.DanceSkill += 1.0f;
                        level.SingingSkill += 1.0f;
                        level.ModelingSkill += 1.0f;
                        level.GroupLevel += 1.0f;
                        level.Energy -= 1.0f;
                        informationMessage(level.ToString());
                    }

                    //every skill will increase by half a point except for group level. energy will decrease
                    if (train == "group")
                    {
                        level.DanceSkill += 0.5f;
                        level.SingingSkill += 0.5f;
                        level.ModelingSkill += 0.5f;
                        level.GroupLevel += 1.5f;
                        level.Energy -= 1.5f;
                        informationMessage(level.ToString());

                    }
                    //for these items, certain skills will increase by .5 and energy will decrease by .5
                    if (train == "headphones")
                    {
                        if (inventory.contents().Contains("headphones"))
                        {
                            level.DanceSkill += 0.75f;
                            level.SingingSkill += 0.75f;
                            level.Energy -= .5f;
                            drop("headphones");
                            informationMessage("*****Item has been dropped back into world*******\n");
                            informationMessage(level.ToString());
                        }

                    }

                    if (train == "mirror")
                    {
                        if (inventory.contents().Contains("mirror"))
                        {
                            level.DanceSkill += 0.75f;
                            level.ModelingSkill += 0.75f;
                            level.Energy -= .5f;
                            drop("mirror");
                            informationMessage("*****Item has been dropped back into world*******\n");
                            informationMessage(level.ToString());
                        }

                    }

                    if (train == "sheetmusic")
                    {
                        if (inventory.contents().Contains("sheetmusic"))
                        {
                            level.DanceSkill += 0.75f;
                            level.SingingSkill += 0.75f;
                            level.Energy -= .5f;
                            drop("sheetmusic");
                            informationMessage("*****Item has been dropped back into world*******\n");
                            informationMessage(level.ToString());
                        }

                    }
                }
                else
                {
                    warningMessage("\nYour energy is too low. Try regaining it before training");
                }
            }
            else
            {
                warningMessage("training type or item can not be used for training. be sure to check your inventory");
            }
        }

        //Allows you to walk into different rooms
        public void waltTo(string direction)
        {

            Door door = this._currentRoom.getExit(direction);
            if (door != null)
            {
                //if the door is open, allow the character to walk through, if not open, receieve an error 
                if (door.State == OCState.Open)
                {
                    
                   
                    visitor = _currentRoom;
                    visit.Push(visitor);
                    
                    Room nextRoom = door.GetRoom(_currentRoom);
                    //just before the player goes to the nextRoom
                    NotificationCenter.Instance.postNotification(new Notification("PlayerWillEnterRoom", this));
                    this._currentRoom = nextRoom;
                     _previousRoom = _currentRoom;
                   
                    //just after player goes to the nextRoom
                    NotificationCenter.Instance.postNotification(new Notification("PlayerDidEnterRoom", this));
                    this.informationMessage("\n" + this._currentRoom.description());
                }
                else
                {
                    this.warningMessage("\nThe door on " + direction + " is not open");
                }
            }

            else
            {
                this.warningMessage("\nThere is no door on " + direction);
            }
        }

        //gives out a message
        public void outputMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void coloredMessage(string message, ConsoleColor color)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            outputMessage(message);
            Console.ForegroundColor = oldColor;
        }
        public void debugMessage(string message)
        {
            coloredMessage(message, ConsoleColor.DarkCyan);
            
        }

        public void warningMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Red);
            
        }

        public void informationMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Blue);
        }

        public void CongratsMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Green);
        }

        public void FailMessage(string message)
        {
            coloredMessage(message, ConsoleColor.Yellow);
        }
    }

}
