using Common.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Item
{
    public class Equipment
    {
        public EquipmentDefine Define { get; set; }
        //¼¼ÄÜTODO
        public Equipment(EquipmentDefine Define)
        {  
            this.Define = Define;
        }
    }
}
