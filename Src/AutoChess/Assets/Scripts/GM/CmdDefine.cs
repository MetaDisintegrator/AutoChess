using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GM
{
    public enum CmdType
    { 
        None =0,
        Set = 1,
        Func = 2,
    }

    public class CmdDefine
    {
        public int ID {  get; set; }
        public string Cmd { get; set; }
        public List<int> Childs { get; set; }
        public CmdType Type { get; set; }
        public CmdFunction Func { get; set; }

        public bool Used {  get; set; } = false;
    }
}