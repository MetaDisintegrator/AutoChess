using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace GM
{
    public class UIGMTool : UIWindow
    {
        public InputField iptCommand;
        public void Submit()
        {
            string[] args = iptCommand.text.Trim().Split(" ");
            GMToolManager.Instance.SendCommand(args);
            iptCommand.text = string.Empty;
        }
    }
}