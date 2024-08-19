using System.Collections;
using System.Collections.Generic;
using Chess;
using Common;
using GameObjects;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class PlayerInputManager : MonoSingleton<PlayerInputManager>
    {
        public delegate void ChessEvent(ChessBase chess);
        public delegate void DragTeamElementEvent(TeamElement element, Vector3 mPos, float delta);

        public event ChessEvent onChessClicked;
        public event DragTeamElementEvent onDragTeamElement;

        public void OnChessClicked(ChessBase chess) => onChessClicked?.Invoke(chess);
        public void OnDragTeamElement(TeamElement element, Vector3 mPos, float delta) => onDragTeamElement?.Invoke(element, mPos, delta);
    }
}

