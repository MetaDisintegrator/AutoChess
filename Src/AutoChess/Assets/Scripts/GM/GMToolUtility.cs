using Common;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GM
{
    public class GMToolUtility : Singleton<GMToolUtility>
    {
        private string DataPath = "Data/";
        
        public void BuildTree(out GMCmdTree root)
        {
            Dictionary<int, CmdDefine> defines = new Dictionary<int, CmdDefine>();
            string json = File.ReadAllText(DataPath + "CmdDefine.txt");
            defines = JsonConvert.DeserializeObject<Dictionary<int, CmdDefine>>(json);
            List<CmdDefine> list = defines.Values.ToList();
            list.Sort(Cmp);

            root = new GMCmdTree()
            {
                Type = CmdType.Set,
                Childs = new Dictionary<string, GMCmdTree>()
            };
            foreach (var def in list)
            {
                if (!def.Used)
                {
                    root.Childs.Add(def.Cmd,Create(def));
                }
            }

            int Cmp(CmdDefine x,CmdDefine y)
            { 
                return y.ID - x.ID;
            }
            GMCmdTree Create(CmdDefine define)
            {
                GMCmdTree tree = new GMCmdTree(define);
                if (define.Type == CmdType.Set)
                {
                    foreach (var id in define.Childs)
                    {
                        CmdDefine childDef = defines[id];
                        tree.Childs.Add(childDef.Cmd, Create(childDef));
                    }
                }
                define.Used = true;
                return tree;
            }
        }
    }
}