using Common.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    public class Shape : ChessBase
    {
        public ShapeDefine Define { get; set; }
        public int Lv { get; set; }
        public int CurrentHP { get; set; }
        //装备列表TODO
        //技能描述列表TODO
    }
}