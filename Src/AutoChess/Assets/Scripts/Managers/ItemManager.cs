using System.Collections;
using System.Collections.Generic;
using Common;
using Common.Data;
using GameObjects;
using Item;
using UnityEngine;

namespace Managers
{
    public class ItemManager : Singleton<ItemManager>
    {
        Dictionary<InventoryID, Inventory> inventories = new Dictionary<InventoryID, Inventory>();

        public delegate void InventoryEvent(InventoryID ID, int idx, Equipment change);
        public event InventoryEvent onInventoryChange;

        public Inventory GetOrCreateInventory(ChessElement owner)
        {
            InventoryID ID = new InventoryID(owner);
            return GetOrCreateInventory(ID, 3);
        }
        public Inventory GetOrCreateBagInventory()
        {
            InventoryID ID = InventoryID.BagInventory;
            return GetOrCreateInventory(ID, 15);
        }
        private Inventory GetOrCreateInventory(InventoryID ID, int capacity)
        {
            if (inventories.TryGetValue(ID, out Inventory inventory))
                return inventory;

            inventory = new Inventory(ID, capacity);
            inventories.Add(ID, inventory);
            return inventory;
        }

        public bool AddItem(InventoryID ID, EquipmentDefine define)
        {
            if (!inventories.TryGetValue(ID, out var inventory))
                return false;
            int idx = inventory.FirstAvailable();
            if (idx > 0)
            {
                Equipment equipment = new Equipment(define);
                PlaceItem(inventory, idx, equipment);
                return true;
            }
            return false;
        }
        public void RemoveItem(InventoryID ID, int idx)
        { 
            if(!inventories.TryGetValue(ID,out var inventory))
                return;
            if (inventory.Remove(idx))
                onInventoryChange?.Invoke(ID, idx, null);
        }

        private bool PlaceItem(Inventory inventory, int idx, Equipment equipment)
        {
            if (inventory.Place(idx, equipment))
            {
                onInventoryChange?.Invoke(inventory.ID, idx, equipment);
                return true;
            }
            return false;
        }
    }
}