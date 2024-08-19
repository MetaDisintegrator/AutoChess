using Chess;
using Common;
using Common.Data;
using GameObjects;
using Res;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Factory
{
    public class ShapeFactory : Common.Singleton<ShapeFactory>
    {
        public TeamElement CreateTeamElement(ShapeDefine define)
        { 
            Shape shape = CreateShape(define);
            GameObject gameObject = ResLoader.Instance.LoadGameObject(define.Resource);
            TeamElement element = null;
            if (gameObject != null)
            {
                element = gameObject.GetOrAddComponent<TeamElement>();
                element.Init();
                element.Type = GameObjects.ChessType.Shape;
                element.Chess = shape;
                element.Area = define.Shape;
            }
            return element;
        }
        public GameObject CreateDragObject(ShapeDefine define)
        {
            GameObject gameObject = ResLoader.Instance.LoadGameObject(define.Resource);
            if(gameObject.TryGetComponent<TeamElement>(out var element))
                Object.Destroy(element);
            if (gameObject.TryGetComponent<BoxCollider2D>(out var collider))
                Object.Destroy(collider);
            return gameObject;
        }

        private Shape CreateShape(ShapeDefine define)
        {
            Shape shape = new Shape()
            {
                Define = define,
                CurrentHP = define.MaxHP[0],
                Lv = 1
            };
            return shape;
        }
    }
}

