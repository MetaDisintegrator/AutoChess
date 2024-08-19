using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStart : MonoBehaviour
{
    public void onStartClicked()
    {
        Debug.Log("New Game Start");
        SceneManager.Instance.LoadScene("Main");
    }
}
