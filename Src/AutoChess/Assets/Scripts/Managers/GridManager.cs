using Common;
using Common.Data;
using GameObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class GridMap
    { 
        public Dictionary<GridID, GridObject> map = new Dictionary<GridID, GridObject>();
        public void Init(GridInitHelper helper)
        {
            foreach (var tran in helper.RestArea)
                DoInit(tran, 2);
            foreach (var tran in helper.FightAreaL)
                DoInit(tran, 5);
            foreach (var tran in helper.FightAreaR)
                DoInit(tran, 5, 5);
        }
        public GridObject this[GridID gridID]
        {
            get
            {
                if (map.TryGetValue(gridID, out var grid))
                { 
                    return grid;
                }
                Debug.LogErrorFormat("GridMap: GridID not Exist {0}");
                return null;
            }
        }

        private void DoInit(Transform transform, int column, int ex =0)
        {
            GridObject grid = null;

            for (int i = 0; i < transform.childCount; i++)
            {
                grid = transform.GetChild(i).GetComponent<GridObject>();
                GridID gridID = new GridID(i % column + 1 + ex, i / column + 1, grid.status, grid.area);
                grid.gridID = gridID;
                map.Add(gridID, grid);
            }
        }
        public List<GridObject> GetGrids(ShapeArea area) => map.Values.Where(obj => obj.area == area).ToList();
    }

    public class GridManager : Singleton<GridManager>
    {
        private GridMap map;
        public GridObject this[GridID gridID] => map[gridID];

        private GridObject currentGrid = null;
        bool gridSeletingMode = false;

        public void SetGridMap(GridMap map)
        { 
            this.map = map;
            foreach (var grid in map.map.Values)
            {
                grid.onMouseEnter += SetCurrentGrid;
            }
        }

        private void SetCurrentGrid(GridObject grid) 
        {
            this.currentGrid = grid;
            if (gridSeletingMode)
                grid.HighlightOn();
        }

        public void EnterGridSelecting() 
        {
            Debug.Log("EnterGridSelecting");
            gridSeletingMode = true;
            if (currentGrid != null)
            {
                if (currentGrid.MouseOver)
                { 
                    currentGrid.HighlightOn();
                }
                else
                    currentGrid = null;
            }
        }
        public bool FinishGridSelecting(out GridObject grid)
        {
            Debug.Log("FinishGridSelecting");
            gridSeletingMode = false;
            if(currentGrid != null && currentGrid.MouseOver)
                grid = currentGrid;
            else
                grid = null;
            return currentGrid != null;
        }

        public Vector3 Grid2WorldPosition(GridID gridID)
        {
            return map[gridID].transform.position;
        }

        public bool FirstAvailable(ShapeArea area,out GridObject grid)
        {
            grid = map.GetGrids(area).First(obj => !obj.Occupied);
            return grid != null;
        }
    }
}