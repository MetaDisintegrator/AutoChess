using Chess;
using Factory;
using Item;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

namespace GameObjects
{
    public class EquipmentElement : EventTrigger
    {
        UnityEngine.UI.Image image;
        bool inDrag = false;

        public InventoryObject InventoryObj {  get; set; }

        public Equipment Item { get; set; }
        float Alpha { 
            get => image.color.a;
            set => image.color = new Color(image.color.r, image.color.g, image.color.b, value);
        }

        public void Init()
        {
            image = GetComponent<UnityEngine.UI.Image>();
        }

        public GameObject EnterDrag(Vector3 screenPos)
        {
            inDrag = true;
            Alpha = 0.5f;
            GameObject go = ItemFactory.Instance.CreateDragObject(Item.Define, transform.parent);
            go.transform.position = screenPos;
            return go;
        }
        public void ExitDrag()
        {
            inDrag = false;
            Alpha = 1f;
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            PlayerInputManager.Instance.OnItemClicked(Item);
        }
        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);

            Vector3 mPos = new Vector3(eventData.position.x, eventData.position.y, 0);
            PlayerInputManager.Instance.OnDragItem(this, mPos, Time.deltaTime);
        }
    }
}
