using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //create a state design to know the state of the door
    public enum OCState { Open, Closed }

    //create an interface that holds the state of the door
    public interface IOCState
    {
        OCState State { get; }
        IOCState open();
        IOCState close();
    }

    //if door is open, get the opened door and have the ability to close it
    public class OpenState : IOCState
    {
        public OCState State { get { return OCState.Open; } }
        public IOCState open()
        {
            return new OpenState();
        }
        public IOCState close()
        {
            return new ClosedState();
        }
    }

    //if the door is closed, reopen a door
    public class ClosedState : IOCState
    {
        public OCState State { get { return OCState.Closed; } }
        public IOCState open()
        {
            return new OpenState();
        }
        public IOCState close()
        {
            return this;
        }
    }

    
    public class Door :IOCState
    {
        //there are two sides to a door. and of course the state of the door
        private Room oneSide;
        private Room otherSide;
        private IOCState _state;

        public OCState State { get { return _state.State; } }

        //constructor method
        public Door(Room one, Room two)
        {
            oneSide = one;
            otherSide = two;
            _state = new OpenState();
        }
        //references the room for door 
        public Room GetRoom(Room from)
        {
            if(from == oneSide)
            {
                return otherSide; 
            }
            else
            {
                return oneSide;
            }
        }

        //if open is called, the state changes to open
        public IOCState open()
        {
            _state = _state.open();
            return _state;
        }
        //if close is called, the state changes to closed
        public IOCState close()
        {
            _state = _state.close();
            return _state;
        }
        //this method was created to create doors but helps reduce the amount of code needed in the gameworld
        public static Door MakeDoor(Room one, Room two, string oneLabel, string twoLabel)
        {
            Door door = new Door(one, two);
            one.setExit(oneLabel, door);
            two.setExit(twoLabel, door);
            return door;

        }
    }
}
