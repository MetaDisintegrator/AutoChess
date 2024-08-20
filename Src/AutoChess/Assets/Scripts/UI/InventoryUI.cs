using Chess;
using Factory;
using GameObjects;
using Item;
using Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class InventoryUI : MonoBehaviour
    {
        public InventoryType type;
        public List<InventoryObject> inventoryObjects;

        InventoryID id;
        bool inited = false;

        private void OnEnable()
        {
            InventoryManager.Instance.RegisterInventory(inventoryObjects);
            ItemManager.Instance.onInventoryChange += UpdateInventory;
        }

        private void OnDisable()
        {
            InventoryManager.Instance.UnRegisterInventory(inventoryObjects);
            if(ItemManager.Instance != null)
                ItemManager.Instance.onInventoryChange -= UpdateInventory;
        }

        private void UpdateInventory(Inventory inventory)
        {
            if (inventory.ID != id)
                return;
            RemoveAll();
            PlaceAll(inventory);
        }
        public void Init(ChessBase owner = null)
        {
            Inventory inventory = null;
            if (type == InventoryType.Bag)
            {
                inventory = ItemManager.Instance.GetOrCreateBagInventory();
            }
            else if (type == InventoryType.Equipments)
            {
                inventory = ItemManager.Instance.GetOrCreateInventory(owner);
            }
            else
            {
                Debug.LogError("InventoryUI: Type not Defined");
            }
            if (!inited)
            {
                id = inventory.ID;
                InitInventory(inventory);
            }
            PlaceAll(inventory);
        }

        private void PlaceAll(Inventory inventory)
        {
            foreach (var pair in inventory.Items)
            {
                PlaceItem(pair.Key, pair.Value);
            }
        }
        public void RemoveAll()
        {
            for (int i = 0; i < inventoryObjects.Count; i++)
            {
                RemoveItem(i);
            }
        }


        private void PlaceItem(int idx ,Equipment item)
        {
            InventoryObject inventory = inventoryObjects[idx];
            inventory.Occupied = true;
            EquipmentElement element = ItemFactory.Instance.CreateItemElement(item, transform);
            element.transform.position = inventory.transform.position;
            inventory.Item = element;
        }
        private void RemoveItem(int idx)
        { 
            InventoryObject inventory = inventoryObjects[idx];
            inventory.Occupied = false;
            if(inventory.Item != null)
            {
                Destroy(inventory.Item.gameObject);
                inventory.Item = null;
            }
        }
        private void InitInventory(Inventory inventory)
        {
            InventoryObject obj;
            for (int i = 0; i < inventoryObjects.Count; i++)
            {
                obj = inventoryObjects[i];
                obj.OwnerInventory = inventory;
                obj.Idx = i;
            }
        }
    }
}