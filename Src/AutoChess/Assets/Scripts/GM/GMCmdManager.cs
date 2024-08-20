using System.Collections;
using System.Collections.Generic;
using System.Text;
using Common;
using Item;
using Managers;
using UnityEngine;

namespace GM
{
    public enum CmdFunction
    {
        None = 0,
        TeamAdd = 2,
        StageRest = 102,
        ItemAdd = 202,
    }
    public class GMCmdManager : Singleton<GMCmdManager>
    {
        public delegate void CmdHandler(string[] args);
        Dictionary<CmdFunction, CmdHandler> handlers = new Dictionary<CmdFunction, CmdHandler>();

        public GMCmdManager()
        {
            handlers.Add(CmdFunction.TeamAdd, TeamAdd);
            handlers.Add(CmdFunction.StageRest, StageRest);
            handlers.Add(CmdFunction.ItemAdd, ItemAdd);
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
            else
                Debug.LogErrorFormat("GMCmd: UnRegistered Command [{0}]", func);
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
        private void ItemAdd(string[] args)
        {
            Log("ItemAdd", args);
            int id = int.Parse(args[0]);
            if (ItemManager.Instance.AddItem(InventoryID.BagInventory, DataManager.Instance.Equipments[id]))
                Debug.Log("Add Success!");
            else 
                Debug.Log("Add Failed!");
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

