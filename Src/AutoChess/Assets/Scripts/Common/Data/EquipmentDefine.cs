using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Common.Data
{
    public class EquipmentDefine
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxHP { get; set; }
        public int Def { get; set; }
        public int Atk { get; set; }
        public int AtkSpeed { get; set; }
        public int AtkRange { get; set; }
        public int Speed { get; set; }
        public int MaxMP { get; set; }
        public int AtkMP { get; set; }
        public int HurtMP { get; set; }
        public int CDRecover { get; set; }
        public List<int> Skills { get; set; }
        
        public string Resource {  get; set; }
        public int Price { get; set; }
        public int SellPrice { get; set; }
    }
}