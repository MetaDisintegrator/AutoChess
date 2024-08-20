using Common;
using Common.Data;
using GameObjects;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Managers
{
    public class InventoryManager : Singleton<InventoryManager>
    {
        private InventoryObject currentSelect = null;
        bool inventorySeletingMode = false;

        public void RegisterInventory(List<InventoryObject> objects)
        {
            foreach (var inventory in objects)
            {
                inventory.onMouseEnter += SetCurrentSelect;
            }
        }
        public void UnRegisterInventory(List<InventoryObject> objects)
        {
            foreach (var inventory in objects)
            {
                inventory.onMouseEnter -= SetCurrentSelect;
            }
        }

        private void SetCurrentSelect(InventoryObject inventory)
        {
            this.currentSelect = inventory;
            if (inventorySeletingMode)
                inventory.HighlightOn();
        }

        public void EnterInventorySelecting()
        {
            Debug.Log("EnterInventorySelecting");
            inventorySeletingMode = true;
            if (currentSelect != null)
            {
                if (currentSelect.MouseOver)
                {
                    currentSelect.HighlightOn();
                }
                else
                    currentSelect = null;
            }
        }

        public bool FinishInventorySelecting(out InventoryObject inventory)
        {
            Debug.Log("FinishInventorySelecting");
            inventorySeletingMode = false;
            if (currentSelect != null && currentSelect.MouseOver)
                inventory = currentSelect;
            else
                inventory = null;
            return inventory != null;
        }
    }
}
