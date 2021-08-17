using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //inherit functions from IITem as this holds them in their own container
    public interface IItemContainer: IItem
    {
        //this will put the items into their own little box
         void put(IItem item);

        //removes them
        IItem remove(string itemName);
        string contents();
    }
    public class ItemContainer : IItemContainer
    {
        //create a dictionary that holds the name of the item and their information
        private Dictionary<string, IItem> items;
        public string Name { get; set; }
        private float _weight;
        
        //gets and updates weight
        public float Weight 
        {
            get
            {
                float tWeight = _weight;
                foreach (IItem item in items.Values)
                {
                    tWeight += item.Weight;
                }
                return tWeight;
                
            }
            set 
            {
                _weight = value;
            }
             
        }
        //gets and updates sellvalue
        public float SellValue { get; set; }

        //gets and updates buyvalue
        public float BuyValue { get; set; }

        //gets and sets wallet
        public float Wallet { get; set; }
        public ItemContainer()
        {
            items = new Dictionary<string, IItem>();
        }
        public void put(IItem item)
        {
            items[item.Name] = item;
        }

        public IItem remove(string itemName)
        {
            IItem item = null;
            items.Remove(itemName, out item);
            return item;
        }
        public void AddDecorator(IItem decorator)
        {

        }

        public string contents()
        { 
             string itemNames = "Items: ";
             Dictionary<string, IItem>.KeyCollection keys = items.Keys;
             foreach (string itemName in keys)
             {
                    itemNames += " " + items[itemName].ToString() + "\n";
              }

                return itemNames;

            }

        }
    }

