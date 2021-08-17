using System.Collections;
using System.Collections.Generic;
using System;

namespace StarterGame
{
    public interface IRoomDelegate
    {
        Room Container { get; set; }
        Door getExit(string exitName);
        string getExits();


    }

    public class Room: IRoomDelegate
    {
        public Room Container { get; set; }

        //create a dictionary that holds the name of the room and door
        private Dictionary<string, Door> exits;
        private string _tag;
        public void PlayerWillBuyItem(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player.currentRoom == Container)
            {
                string word = (string)notification.userInfo["word"];
                if (word != null)
                {

                    Container.Delegate = null;

                }
            }
        }
        public void PlayerDidBuyItem(Notification notification)
        {
            Player player = (Player)notification.Object;
            if (player.currentRoom == Container)
            {
                string word = (string)notification.userInfo["word"];
                if (word != null)
                {

                    Container.Delegate = null;

                }
            }
        }
        //the name of the the room will be stored under tag
        public string tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        //create a iroomdelegate
        private IRoomDelegate _delegate;
        public IRoomDelegate Delegate
        {
            get
            {
                return _delegate;
            }
            set
            {
                _delegate = value;
            }
        }

        //call itemcontainer into the program
        private ItemContainer itemContainer;

        //constructor method for a room with no exits
        public Room() : this("No Tag")
        {

        }
       

        //constructor for rooms with exits
        public Room(string tag)
        {
            exits = new Dictionary<string, Door>();
            this.tag = tag;
            _delegate = null;
            itemContainer = new  ItemContainer();
            Dictionary<float, Object> inventory = new Dictionary<float, object>();
            NotificationCenter.Instance.addObserver("PlayerWillBuyItem", PlayerWillBuyItem);
            NotificationCenter.Instance.addObserver("PlayerDidBuyItem", PlayerDidBuyItem);
        }

        //this drops items into the rooms
        public void drop(IItem item)
        {
            itemContainer.put(item);
        }

        //this picksup an item in a room
        public IItem pickup(string itemName)
        {
            return itemContainer.remove(itemName);
        }

        //returns all the items 
        public string getItems()
        {
            return itemContainer.contents();
        }

        //creates a door in betwwn rooms
        public void setExit(string exitName, Door door)
        {
            exits[exitName] = door;
        }

        //returns the current exit
        public Door getExit(string exitName)
        {
            if(_delegate != null)
            {
                return _delegate.getExit(exitName);
            }
            else
            {
                return getExit(exitName, null);
                /*
                Room room = null;
                exits.TryGetValue(exitName, out room);
                return room;
                */
            }
        }
        
        //returns exit if the room is a delegate
        public Door getExit(string exitName, IRoomDelegate rDelegate)
        {
            if(rDelegate == Delegate)
            {
                Door door = null;
                exits.TryGetValue(exitName, out door);
                return door;
            }
            else
            {
                return null;
            }
        }

        //returns all the current exits
        public string getExits()
        {
            if(_delegate != null)
            {
                return _delegate.getExits();
            }
            else
            {  
                string exitNames = "Exits: ";
                Dictionary<string, Door>.KeyCollection keys = exits.Keys;
                foreach (string exitName in keys)
                {
                     exitNames += " " + exitName;
                }

                return exitNames;

            }
          
        }

        //describes where you are
        public string description()
        {
            return "You are " + this.tag + ".\n *** " + this.getExits() + "\n +++ " + this.getItems();
        }
    }
}
