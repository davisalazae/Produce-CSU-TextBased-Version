using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    class GameWorld
    {
        //belongs to gameworld class
        static private GameWorld _instance = null;
        
        static public GameWorld Instance
        {
            get
            {
                if(_instance == null)
                {
                    //lazy instantiation= only instantiate when called

                    _instance = new GameWorld();
                }
                return _instance;
            }
        }
        //internally provided an entrance to the world
        private Room _entrance;
        private Room _start;
        private Room _trigger;
        private Room _trainingRoom1;
        private Room _trainingRoom2;
        private Room _trainingRoom3;
        private Room _trainingRoom4;
        private Room _fromRoom;
        private Room _toRoom;
        private Room _debut;
        private Room _characterCreate;
        //provided property access only as a getter
        public Room Entrance => _entrance;
        public Room CharacterCreate => _characterCreate;
        public Room TrainingRoom1 => _trainingRoom1; 
        public Room TrainingRoom2 =>_trainingRoom2;
        public Room TrainingRoom3 => _trainingRoom3;
        public Room TrainingRoom4 => _trainingRoom4;
        public Room DebutRoom => _debut;
        public Room Trigger => _trigger;
        public Room Start
        {
            get
            {
                return _start;
            }
        }

      

        //calls createworld into entrance so that the class cannot be accessed by players

        public GameWorld()
        {
            createWorld();
            NotificationCenter.Instance.addObserver("PlayerDidEnterRoom", PlayerWillEnterRoom);
        
        }

        public void PlayerWillEnterRoom(Notification notification)
        {
            Player player = (Player)notification.Object;
            Room room = player.currentRoom;
            if(room == _trigger)
            {
                Console.WriteLine("****The player is now in the trigger room.****");
                Door door = Door.MakeDoor(_fromRoom, _toRoom, "west", "east");
               
            }
        }
        private void createWorld()
        {
            //create rooms
            Room outside = new Room("outside the main entrance of CSU Entertainment.It's so beautiful outside.");
            Room eastWing = new Room("in east wing hallway.");
            Room westWing = new Room("in west wing hallway.");
            Room danceStudio = new Room("in the dance studio. Are you here to practice?");
            Room singingStudio= new Room("in the singing studio. Are you here to practice? ");
            Room home = new Room("in your home. Maybe you should get some rest?");
            Room modelingRoom = new Room("in the modeling room.");
            Room managerOffice = new Room("in the manager's office");
            Room shop = new Room("Welcome to the Cougar Bodega! Feel free to browse our selection");
            Room saveRoom = new Room("in the save room.Would you like to save?");
            Room stage = new Room("This is the big moment! You are onstage.");
            Room mall = new Room("in the mall. Winding down would be nice");
            Room lobby = new Room("In the lobby. Use your commands to go to your destination");
            Room characterCreate = new Room("Welcome to character creation. Use your commands to setup your character");
           
            //create doors to rooms

            Door door = Door.MakeDoor(outside, home, "west", "east");
            door = Door.MakeDoor(outside, characterCreate, "south", "north");
            door = Door.MakeDoor(outside, mall, "east", "west");
            door = Door.MakeDoor(outside, lobby, "north", "south" );
       
            door = Door.MakeDoor(lobby, eastWing, "east", "west");
            door = Door.MakeDoor(eastWing, modelingRoom, "east", "west");
            door = Door.MakeDoor(eastWing, danceStudio, "south", "north");
            door = Door.MakeDoor(eastWing, singingStudio, "north", "south");

            door = Door.MakeDoor(lobby, westWing, "west", "east");
            door = Door.MakeDoor(westWing, shop, "west", "east");
            door = Door.MakeDoor(westWing, managerOffice, "north", "south");
            door = Door.MakeDoor(lobby, stage, "north", "south");
            door.close();
           
            //Make assignments to special rooms
            _characterCreate= characterCreate;
            _trigger = shop;
            _trigger = mall;
            _debut = stage;
            _entrance = outside;
            _trainingRoom1 = modelingRoom;
            _trainingRoom2 = singingStudio;
            _trainingRoom3 = home;
            _trainingRoom4 = danceStudio;
            
            _toRoom = westWing;
            _fromRoom = shop;
           
            IItem plant = new Item("plant", 3.5f, 15f, 15f);
            IItem decor = new Item("picture", 5f, 3.5f, 3.5f);
            IItem bookbag = new Item("bookbag", 10.0f, 40f, 50f);
            IItem chair = new Item("chair", 10.0f, 50f, 50f);
            IItem Bed = new Item("bed", 50f, 400f, 500f);
            IItem fluffycushion = new Item("cushion", 2.5f, 15f, 10f);
            IItem tables = new Item("table", 10f, 50f, 50f);
            IItem pretzel = new Item("pretzel", 0.5f, 3.5f, 3.5f);
            IItem icecream = new Item("icecream", 0.5f, 1.5f, 1.5f);
            IItem burger = new Item("burger", 0.5f, 3.5f, 3.5f);
            IItem music = new Item("sheetmusic", 2.5f, 3.5f, 3.5f);
            IItem headphones = new Item("headphones", 2.8f, 20f, 20f);
            IItem mirror = new Item("mirror", 3.0f, 10f, 10f);
           

            //Bed Room items 
            home.drop(bookbag);
            chair.AddDecorator(fluffycushion);
            home.drop(chair);
            home.drop(Bed);
            home.drop(music);
            home.drop(headphones);
            home.drop(mirror);

            //mall items
            mall.drop(chair);
            mall.drop(tables);
            mall.drop(pretzel);
            mall.drop(icecream);
            mall.drop(burger);

            //lobby items
            lobby.drop(chair);
            lobby.drop(tables);
            lobby.drop(plant);
            lobby.drop(decor);

            //managerOffice items
            managerOffice.drop(chair);
            managerOffice.drop(tables);
            managerOffice.drop(decor);
            managerOffice.drop(music);

            //modelingRoom item
            modelingRoom.drop(mirror);
            modelingRoom.drop(chair);

            //singingStudio items
            singingStudio.drop(music);
            singingStudio.drop(headphones);

            //dancestudio items
            danceStudio.drop(headphones);
            danceStudio.drop(mirror);

            //bodega items
            IBodega chips = new Bodega("chips", 0.5f, 1.5f);
            IBodega sandwich = new Bodega("sub", 1.0f, 4.0f);
            IBodega granola = new Bodega("granola", 1.0f, 1.75f);

            IItem saltnvinegar = new Item("salt n Vinegar", 0.0f, 0.6f, 1.3f);
           

            chips.AddDecorator(saltnvinegar);
            shop.drop(chips);
            shop.drop(sandwich);
            shop.drop(granola);

        }

               
        }
 }

