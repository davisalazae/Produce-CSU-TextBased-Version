using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //this creates an interface of functions for items
    public interface IItem
    {
        string Name { get; set; }
        float Weight { get; set; }
        float SellValue { get; set; }
        float BuyValue { get; set; }

        float Wallet { get; set; }
        void AddDecorator(IItem decorator);
    
        string ToString();
    }

    public class Item : IItem
    {
        public string Name { get; set; }
        
        private float _weight;
        
        //if there is no change to weight, get its current value but if there is a change, increment or decrement it
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

        //if there is no change to sellvalue, get its current value but if there is a change, increment or decrement it
        private float _sellValue;
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

        //if there is no change to wallet, get its current value but if there is a change, increment or decrement it
        private float _walletValue=12f;
        public float Wallet
        {
            get
            {
                return _walletValue + (_decorator == null ? 0 : _decorator.Wallet);
            }
            set
            {
                _walletValue = value;
            }
        } 

        public float BuyValue { get; set; } 
        
        private IItem _decorator;
        
        //predetermined placement for method calling
        public Item() : this("Nameless", 1.0f, 0.1f, 0.5f)
        {

        }

        //constructor method
        public Item(string name, float weight, float sellValue, float buyValue)
        {
            Name = name;
            Weight = weight;
            SellValue = sellValue;
            BuyValue = buyValue;

            _decorator = null;
        }
        

        //if there is no change to decorator, add decorator to item
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
            return Name + ", " + Weight + ", sv: "+ SellValue + ", bv: " + BuyValue + "\nCash: $"+ Wallet;
        }
    }
}
