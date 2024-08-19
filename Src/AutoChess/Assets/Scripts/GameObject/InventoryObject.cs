using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects
{
    public class InventoryObject : MonoBehaviour
    {
        Image image;
        Color originColor;

        public bool MouseOver { get; private set; }
        public bool Occupied { get; set; } = false;

        public delegate void InventoryEvent(InventoryObject grid);
        public event InventoryEvent onMouseEnter;
        public event InventoryEvent onMouseExit;

        private void Start()
        {
            image = GetComponent<Image>();
            originColor = image.color;
        }

        private void OnMouseEnter()
        {
            MouseOver = true;
            onMouseEnter?.Invoke(this);
        }
        private void OnMouseExit()
        {
            MouseOver = false;
            HighlightOff();
            onMouseExit?.Invoke(this);
        }

        public void HighlightOn()
        {
            image.color = Color.yellow;
        }
        public void HighlightOff()
        {
            image.color = originColor;
        }
    }
}
