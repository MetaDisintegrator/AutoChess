using Chess;
using Common;
using Common.Data;
using GameObjects;
using Item;
using Res;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Factory
{
    public class ItemFactory : Common.Singleton<ItemFactory>
    {
        public GameObject CreateDragObject(EquipmentDefine define, Transform root)
        {
            GameObject gameObject = ResLoader.Instance.LoadGameObject(define.Resource, root: root);
            if (gameObject.TryGetComponent<EquipmentElement>(out var element))
                Object.Destroy(element);
            if (gameObject.TryGetComponent<Image>(out var image))
                image.raycastTarget = false;
            return gameObject;
        }
        public EquipmentElement CreateItemElement(Equipment equipment, Transform root)
        {
            GameObject gameObject = ResLoader.Instance.LoadGameObject(equipment.Define.Resource, root: root);
            EquipmentElement element = null;
            if (gameObject != null)
            {
                element = gameObject.GetOrAddComponent<EquipmentElement>();
                element.Init();
                element.Item = equipment;
            }
            return element;
        }
    }
}
