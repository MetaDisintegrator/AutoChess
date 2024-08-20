using Chess;
using GameObjects;
using System;
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
        public ChessBase Owner { get; set; }
        public InventoryID(ChessBase owner)
        {
            Type = InventoryType.Equipments;
            Owner = owner;
        }

        public static InventoryID BagInventory = new InventoryID()
        {
            Type = InventoryType.Bag,
            Owner = null,
        };

        public static bool operator ==(InventoryID a, InventoryID b) => a.Equals(b);
        public static bool operator !=(InventoryID a, InventoryID b) => !a.Equals(b);

        public override bool Equals(object obj)
        {
            return obj is InventoryID ID &&
                   Type == ID.Type &&
                   EqualityComparer<ChessBase>.Default.Equals(Owner, ID.Owner);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Owner);
        }
    }
    public class Inventory
    {
        public InventoryID ID { get; set; }
        int Capacity;
        Dictionary<int, Equipment> _items = new Dictionary<int, Equipment>();
        public Dictionary<int, Equipment> Items => _items;

        bool IsFull() => _items.Count >= Capacity;

        public Inventory(InventoryID ID, int capacity)
        {
            this.ID = ID;
            this.Capacity = capacity;
        }
        public int FirstAvailable()
        {
            for (int i = 0; i < Capacity; i++)
            {
                if (!_items.ContainsKey(i))
                    return i;
            }
            return -1;
        }
        public bool Place(int idx, Equipment item)
        {
            if (_items.ContainsKey(idx))
                return false;
            _items.Add(idx, item);
            return true;
        }
        public bool Remove(int idx)
        { 
            if(!_items.ContainsKey(idx))
                return false;
            _items.Remove(idx);
            return true;
        }
    }
}
