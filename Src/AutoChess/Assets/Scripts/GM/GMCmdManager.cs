using System.Collections;
using System.Collections.Generic;
using System.Text;
using Common;
using Managers;
using UnityEngine;

namespace GM
{
    public enum CmdFunction
    {
        None = 0,
        TeamAdd = 2,
        StageRest = 102,
    }
    public class GMCmdManager : Singleton<GMCmdManager>
    {
        public delegate void CmdHandler(string[] args);
        Dictionary<CmdFunction, CmdHandler> handlers = new Dictionary<CmdFunction, CmdHandler>();

        public GMCmdManager()
        {
            handlers.Add(CmdFunction.TeamAdd, TeamAdd);
            handlers.Add(CmdFunction.StageRest, StageRest);
        }

        public void Handle(CmdFunction func, string[] args)
        {
            if (handlers.TryGetValue(func, out CmdHandler handler))
            {
                if (handler != null)
                {
                    handler(args);
                }
            }
        }

        private void TeamAdd(string[] args)
        {
            Log("TeamAdd", args);
            int id = int.Parse(args[0]);
            TeamManager.Instance.AddMember(DataManager.Instance.Shapes[id], out var buf);
        }

        private void StageRest(string[] args)
        {
            Log("StageRest", args);
            bool token = bool.Parse(args[0]);
            if (token)
                GameManager.Instance.EnterRestStage();
            else
                GameManager.Instance.ExitRestStage();
        }

        private void Log(string func, string[] args)
        { 
            StringBuilder builder = new StringBuilder(func + ": ");
            foreach (string arg in args)
            {
                builder.Append(arg+" ");
            }
            Debug.Log(builder.ToString());
        }
    }
}

