using GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public enum InventoryType
    {
        None = 0,
        Bag = 1,
        Equipments = 2,
    }
    public struct InventoryID
    {
        public InventoryType Type { get; set; }
        public ChessElement Owner { get; set; }
        public InventoryID(ChessElement owner)
        {
            Type = InventoryType.Equipments;
            Owner = owner;
        }

        public static InventoryID BagInventory = new InventoryID()
        {
            Type = InventoryType.Bag,
            Owner = null,
        };
    }
    public class Inventory
    {
        public InventoryID ID { get; set; }
        int Capacity;
        Dictionary<int, Equipment> items = new Dictionary<int, Equipment>();

        bool IsFull() => items.Count >= Capacity;

        public Inventory(InventoryID ID, int capacity)
        {
            this.ID = ID;
            this.Capacity = capacity;
        }
        public int FirstAvailable()
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (!items.ContainsKey(i))
                    return i;
            }
            return -1;
        }
        public bool Place(int idx, Equipment item)
        {
            if (items.ContainsKey(idx))
                return false;
            items.Add(idx, item);
            return true;
        }
        public bool Remove(int idx)
        { 
            if(!items.ContainsKey(idx))
                return false;
            items.Remove(idx);
            return true;
        }
    }
}
