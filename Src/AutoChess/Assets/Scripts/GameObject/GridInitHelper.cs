using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridInitHelper : MonoBehaviour
{
    public Transform[] RestArea;
    public Transform[] FightAreaL;
    public Transform[] FightAreaR;

    void Start()
    {
        GridMap gridMap = new GridMap();
        gridMap.Init(this);
        GridManager.Instance.SetGridMap(gridMap);
    }
}
