using GM;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("Open GMTool");
            UIManager.Instance.Show<UIGMTool>();
        }
    }
}