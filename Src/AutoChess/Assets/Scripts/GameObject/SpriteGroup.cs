using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class SpriteGroup : MonoBehaviour
    {
        public List<SpriteRenderer> renderers;
        float _alpha = 1f;
        public float Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                foreach (var renderer in renderers)
                {
                    renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, value);
                }
            }
        }
    }
}
