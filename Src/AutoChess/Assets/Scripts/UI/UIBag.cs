using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class UIBag : MonoBehaviour
    {
        public InventoryUI inventory;
        private void OnEnable()
        {
            inventory.Init();
        }
    }
}