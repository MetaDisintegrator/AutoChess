using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Data;
using Newtonsoft.Json;
using System.IO;
using System;

namespace GM
{
    public class GMCmdTree
    {
        public Dictionary<string,GMCmdTree> Childs = null;
        public GMCmdTree this[string cmd]
        {
            get
            {
                if(Type == CmdType.Func)
                    return null;
                if (this.Childs != null && Childs.TryGetValue(cmd, out var tree))
                { 
                    return tree;
                }
                return null;
            }
        }

        public GMCmdTree(CmdDefine define)
        { 
            Type = define.Type;
            Func = define.Func;
            if (Type == CmdType.Set)
                Childs = new Dictionary<string, GMCmdTree>();
        }
        public GMCmdTree() { }

        public CmdType Type {  get; set; }
        public CmdFunction Func { get; set; }
    }

    public class GMToolManager : Singleton<GMToolManager>
    {
        GMCmdTree cmdRoot;
        
        public GMToolManager()
        { 
                Init();
        }
        public void Init()
        {
            GMToolUtility.Instance.BuildTree(out cmdRoot);
        }

        public void SendCommand(params string[] args)
        {
            ProcessCmd(cmdRoot, args, 0);

            void ProcessCmd(GMCmdTree tree, string[] args, int idx)
            {
                if (tree.Type == CmdType.Set)
                {
                    GMCmdTree c = tree[args[idx]];
                    if (c != null)
                        ProcessCmd(c, args, idx + 1);
                    else
                        Debug.LogErrorFormat("GMTool: Unknown Command: {0}", args[idx]);
                }
                else
                {
                    string[] buf = new string[args.Length - idx];
                    Array.Copy(args, idx, buf, 0, args.Length - idx);
                    GMCmdManager.Instance.Handle(tree.Func, buf);
                }
            }
        }
    }
}