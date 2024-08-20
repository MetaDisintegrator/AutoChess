using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Res
{
    public class ResLoader : Singleton<ResLoader>
    {
        public GameObject LoadGameObject(string path, bool init = true, Transform root = null)
        { 
            GameObject prefab = Resources.Load<GameObject>(path);
            if (prefab == null)
            {
                Debug.LogErrorFormat("ResLoader: Prefab not Exist [{0}]", path);
                return null;
            }
            GameObject obj = GameObject.Instantiate(prefab);
            if (root != null)
                obj.transform.SetParent(root);
            if (init)
            {
                obj.transform.position = Vector3.zero;
                obj.transform.rotation = Quaternion.identity;
                obj.transform.localScale = Vector3.one;
            }
            return obj;
        }
    }
}

