using System.Collections;
using System.Collections.Generic;
using Chess;
using Common;
using Common.Data;
using GameObjects;
using Item;
using UnityEngine;

namespace Managers
{
    public class ItemManager : MonoSingleton<ItemManager>
    {
        bool active = true;
        Dictionary<InventoryID, Inventory> inventories = new Dictionary<InventoryID, Inventory>();

        public delegate void InventoryEvent(Inventory inventory);
        public event InventoryEvent onInventoryChange;

        public Inventory GetOrCreateInventory(ChessBase owner)
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
            if (idx >= 0)
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
                onInventoryChange?.Invoke(inventory);
        }

        private bool PlaceItem(Inventory inventory, int idx, Equipment equipment)
        {
            if (inventory.Place(idx, equipment))
            {
                onInventoryChange?.Invoke(inventory);
                return true;
            }
            return false;
        }
        private void ReplaceItem(EquipmentElement dragElement, InventoryObject inventory)
        {
            InventoryObject oldInventory = dragElement.InventoryObj;
            PlaceItem(inventory.OwnerInventory, inventory.Idx, dragElement.Item);
            RemoveItem(oldInventory.OwnerInventory.ID, oldInventory.Idx);
        }

        public void EnterRestStage()
        {
            active = true;
            PlayerInputManager.Instance.onItemClicked += OnClickItem;
            PlayerInputManager.Instance.onDragItem += OnDragItem;
        }
        public void ExitRestStage()
        {
            active = false;
            PlayerInputManager.Instance.onItemClicked -= OnClickItem;
            PlayerInputManager.Instance.onDragItem -= OnDragItem;
        }

        float dragTime;
        EquipmentElement dragElement = null;
        GameObject dragObj = null;

        private void OnClickItem(Equipment item)
        {
            Debug.Log("ItemManager: ClickItem");
        }
        private void OnDragItem(EquipmentElement element, Vector3 mPos, float delta)
        {
            if (dragElement != null)
            {
                dragObj.transform.position = mPos;
            }
            else
            {
                dragElement = element;
                dragObj = element.EnterDrag(mPos);
                InventoryManager.Instance.EnterInventorySelecting();
                dragTime = 0f;
            }
            //Debug.LogFormat("TeamManager: DragMember {0}",mPos);

        }
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                dragTime = 0;
                if (dragElement != null)
                {
                    if (InventoryManager.Instance.FinishInventorySelecting(out var inventory))
                    {
                        Debug.Log("ItemManager: Item Move Done!");
                        ReplaceItem(dragElement, inventory);
                    }
                    dragElement.ExitDrag();
                    dragElement = null;
                    Destroy(dragObj);
                    dragObj = null;
                }
            }
        }
    }
}