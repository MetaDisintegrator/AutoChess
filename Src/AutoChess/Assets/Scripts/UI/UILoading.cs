using Managers;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class UILoading : MonoBehaviour
{
    private void Start()
    {
        DataManager.Instance.Load();

        //StringBuilder builder = new();
        //builder.AppendLine(string.Format("Shape Load First:{0}", DataManager.Instance.Shapes[1]));
        //builder.AppendLine(string.Format("Skill Load First:{0}", DataManager.Instance.Skills[1]));
        //Debug.Log(builder.ToString());

        SceneManager.Instance.LoadScene("Start");
    }
}
