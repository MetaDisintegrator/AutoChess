using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data
{
    public enum ShapeArea
    { 
        None = 0,
        Square = 1,
        Rhombus = 2,
        Triangle =3,
    }

    public class ShapeDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ShapeArea Shape { get; set; }
        public List<int> MaxHP { get; set; }
        public List<int> Def { get; set; }
        public List<int> Atk { get; set; }
        public List<int> AtkSpeed { get; set; }
        public List<int> AtkRange { get; set; }
        public List<int> Speed { get; set; }
        public List<int> MaxMP { get; set; }
        public List<int> AtkMP { get; set; }
        public List<int> HurtMP { get; set; }
        public string Resource { get; set; }
        public List<int> Skill { get; set; }
        public int Price { get; set; }
        public int SellPrice { get; set; }

        public override string ToString()
        {
            return string.Format("Shape({0}:{1},{2})", ID, Name, Description);
        }
    }
}

