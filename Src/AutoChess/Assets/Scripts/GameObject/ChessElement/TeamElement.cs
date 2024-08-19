using Chess;
using Common.Data;
using Factory;
using GameObjects;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class TeamElement : ChessElement
    {
        public int membereId;
        public FightStatus Status { get; set; }
        public Shape Shape => Chess as Shape;

        bool inDrag = false;
        SpriteGroup spriteGroup;

        public void Init()
        {
            spriteGroup = GetComponent<SpriteGroup>();
        }

        public GameObject EnterDrag(Vector3 mPos)
        { 
            inDrag = true;
            spriteGroup.Alpha = 0.5f;
            GameObject go = ShapeFactory.Instance.CreateDragObject(Shape.Define);
            go.transform.position = mPos;
            return go;
        }
        public void ExitDrag()
        { 
            inDrag = false;
            spriteGroup.Alpha = 1f;
        }

        private void OnMouseDrag()
        {
            Vector3 mPos = Input.mousePosition;
            PlayerInputManager.Instance.OnDragTeamElement(this, mPos, Time.deltaTime);
        }
    }
}