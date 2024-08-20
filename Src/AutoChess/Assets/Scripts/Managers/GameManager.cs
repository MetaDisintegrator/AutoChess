using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool InRestStage;

        public void EnterRestStage()
        { 
            InRestStage = true;
            TeamManager.Instance.EnterRestStage();
            ItemManager.Instance.EnterRestStage();
        }

        public void ExitRestStage()
        {
            InRestStage = false;
            TeamManager.Instance.ExitRestStage();
            ItemManager.Instance.ExitRestStage();
        }
    }
}
