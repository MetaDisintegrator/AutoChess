using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CanvasGetter : MonoBehaviour
    {
        void Start()
        {
            UIManager.Instance.Canvas = transform;
        }
    }
}