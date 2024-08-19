using Chess;
using Common.Data;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public enum ChessType
    {
        None =0,
        Shape = 1,
        Enemy = 2,
    }
    public class ChessElement : MonoBehaviour
    {
        public int chessID;
        public ChessBase Chess { get; set; }
        public ShapeArea Area { get; set; }
        public GridObject Grid { get; set; }
        public ChessType Type { get; set; }

        private void OnMouseUpAsButton()
        {
            PlayerInputManager.Instance.OnChessClicked(Chess);
        }
    }
}

