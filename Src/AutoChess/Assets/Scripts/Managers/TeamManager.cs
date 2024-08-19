using Chess;
using Common;
using Common.Data;
using Factory;
using GameObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class TeamManager : MonoSingleton<TeamManager>
    {
        private Dictionary<int, TeamElement> members = new Dictionary<int, TeamElement>();

        const int Rest_Place_Limit = 5;
        bool active = true;

        private int nextID = 1;

        public bool AddMember(ShapeDefine shape,out TeamElement element)
        {
            element = null;
            if (GridManager.Instance.FirstAvailable(shape.Shape, out var grid))
            {
                element = ShapeFactory.Instance.CreateTeamElement(shape);
                element.membereId = nextID++;
                PlaceMember(element, grid);
                return true;
            }
            return false;
        }
        public void RemoveMember(int memberID)
        {
            if (members.TryGetValue(memberID, out var element))
            {
                GameObject.Destroy(element.gameObject);
                members.Remove(memberID);
            }
        }

        private void ReplaceMember(TeamElement element, GridObject grid)
        {
            element.Grid.Occupied = false;
            PlaceMember(element, grid);
        }
        private void PlaceMember(TeamElement element, GridObject grid)
        {
            grid.Occupied = true;
            element.Area = grid.area;
            element.Status = grid.status;
            element.Grid = grid;
            element.transform.position = grid.transform.position + Vector3.down * 3f;
        }

        public void EnterRestStage() 
        {
            active = true;
            PlayerInputManager.Instance.onChessClicked += OnClickMember;
            PlayerInputManager.Instance.onDragTeamElement += OnDragMember;
        }
        public void ExitRestStage() 
        {
            active = false;
            PlayerInputManager.Instance.onChessClicked -= OnClickMember;
            PlayerInputManager.Instance.onDragTeamElement -= OnDragMember;
        }
        private void TurnLimitLine(bool on)
        {
        }

        float dragTime;
        TeamElement dragElement = null;
        GameObject dragObj = null;

        private void OnClickMember(ChessBase chess)
        {
            Debug.Log("TeamManager: ClickMember");
        }
        private void OnDragMember(TeamElement element, Vector3 mPos, float delta)
        {
            if (dragElement != null)
            { 
                Vector3 pos = Camera.main.ScreenToWorldPoint(mPos);
                dragObj.transform.position = new Vector3(pos.x, pos.y, 0);
            }
            else
            {
                if (dragTime > 0.08f)
                {
                    dragElement = element;
                    dragObj = element.EnterDrag(mPos);
                    GridManager.Instance.EnterGridSelecting();
                    dragTime = 0f;
                }
                else
                {
                    dragTime += delta;
                }
            }
            //Debug.LogFormat("TeamManager: DragMember {0}",mPos);
            
        }
        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                dragTime = 0;
                if (dragElement != null)
                {
                    if (GridManager.Instance.FinishGridSelecting(out var grid))
                    {
                        Debug.Log("TeamManager: Member Move Done!");
                        ReplaceMember(dragElement, grid);
                    }
                    dragElement.ExitDrag();
                    dragElement = null;
                    Destroy(dragObj);
                    dragObj = null;
                }
            }
        }
    }
}