using System;
using System.Collections.Generic;
using System.Text;

namespace StarterGame
{
    //create interface that implements functions to the IBodega class
        public interface IBodegaContainer : IBodega
        {
            void put(IBodega item);

            IBodega remove(string itemName);
            string contents();
        }
        
        //create a dictionary to hold each container
        public class BodegaContainer : IBodegaContainer
        {
            private Dictionary<string, IBodega> inventory;
             public Room Container { get; set; }
            public float Wallet { get; set; }
            public Door getExit(string exitName)
            {
                  return null;
            }
            public string getExits()
             {
                return "\nPlease make a choice.\n";
             }
             public string Name { get; set; }
            private float _weight;
            
            
            public float Weight
            {
                get
                {
                    float tWeight = _weight;
                    foreach (IBodega products in inventory.Values)
                    {
                        tWeight += products.Weight;
                    }
                    return tWeight;

                }
                set
                {
                    _weight = value;
                }

            }
            public float SellValue { get; set; }
            public float BuyValue { get; set; }
            public BodegaContainer()
            {
                inventory = new Dictionary<string, IBodega>();
            }
            public void put(IBodega item)
            {
                inventory[item.Name] = item;
            }

            public IBodega remove(string itemName)
            {
                IBodega item = null;
                inventory.Remove(itemName, out item);
                return item;
            }
            public void AddDecorator(IItem decorator)
            {

            }

            public string contents()
            {
                string itemNames = "Items: ";
                Dictionary<string, IBodega>.KeyCollection keys = inventory.Keys;
                foreach (string itemName in keys)
                {
                    itemNames += " " + inventory[itemName].ToString() + "\n";
                }

                return itemNames;

            }

        }
    }

