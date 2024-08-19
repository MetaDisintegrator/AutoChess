using Common.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameObjects
{
    public enum FightStatus
    { 
        None = 0,
        Rest = 1,
        Fight = 2,
    }

    public struct Vector2Int
    { 
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }
    }
    public struct GridID
    { 
        public Vector2Int Pos { get; set; }
        public FightStatus Status { get; set; }
        public ShapeArea Area { get; set; }
        public GridID(int x,int y, FightStatus status, ShapeArea area)
        {
            Pos = new Vector2Int(x, y);
            Status = status;
            Area = area;
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2},{3})", Pos.X, Pos.Y, Area, Status);
        }
    }

    public class GridObject : MonoBehaviour
    {
        SpriteRenderer render;
        Color originColor;

        public GridID gridID;
        public ShapeArea area;
        public FightStatus status;
        public bool MouseOver { get; private set; }
        public bool Occupied { get; set; } = false;

        public delegate void GridEvent(GridObject grid);
        public event GridEvent onMouseEnter; 
        public event GridEvent onMouseExit;

        private void Start()
        {
            render = GetComponent<SpriteRenderer>();
            originColor = render.color;
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
            render.color = Color.yellow;
        }
        public void HighlightOff() 
        {
            render.color = originColor;
        }
    }
}