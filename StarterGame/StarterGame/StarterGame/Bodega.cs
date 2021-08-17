using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //make an interface that adopts the functions from iitem
    public interface IBodega : IItem
    {
 
     
    }

    //class inherits the functions of Ibodega
    public class Bodega: IBodega
    {
        //get and set the name
        public string Name { get; set; }

        private float _weight;
        //if weight doesn't change, get it else increment it and set it
        public float Weight
        {
            get
            {
                if (_decorator == null)
                {
                    return _weight;
                }
                else
                {
                    return _weight + _decorator.Weight;
                }

            }
            set
            {
                _weight = value;
            }
        }

        private float _sellValue;

        //if sellValue doesn't change, get it else increment it and set it
        public float SellValue
        {
            get
            {
                return _sellValue + (_decorator == null ? 0 : _decorator.SellValue);
            }
            set
            {
                _sellValue = value;
            }
        }

        private float _buyValue;

        //if buyvalue doesn't change, get it else increment it and set it
        public float BuyValue
        {
            get
            {
                return _buyValue + (_decorator == null ? 0 : _decorator.BuyValue);
            }
            set
            {
                _buyValue = value;
            }
        }

        private float _walletValue;

        //if wallet doesn't change, get it else increment it and set it
        public float Wallet
        {
            get
            {
                return _walletValue - (_decorator == null ? 0 : _decorator.Wallet);
            }
            set
            {
                _walletValue = value;
            }
        }

        

        private IItem _decorator;

        //create formatting for the bodega method call
        public Bodega() : this("Nameless", 0.0f, 0.0f)
        {


        }

        //constructor
        public Bodega(string name, float weight, float buyValue)
        {
            Name = name;
            Weight = weight;
            BuyValue = buyValue;
           
            _decorator = null;
        }

        //Ability to add Items onto others
        public void AddDecorator(IItem decorator)
        {
            if (_decorator == null)
            {
                _decorator = decorator;
            }
            else
            {
                _decorator.AddDecorator(decorator);
            }
        }


        override
        public string ToString()
        {
            return Name + ", " + Weight + ", Cost: " + BuyValue ;
        }

        public Door getExit(string exitName)
        {
            return null;
        }

        public string getExits()
        {
            return "Please make a choice\n";
        }
    }
}
