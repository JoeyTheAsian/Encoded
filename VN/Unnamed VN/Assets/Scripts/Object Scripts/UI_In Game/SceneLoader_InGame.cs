using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader_InGame : MonoBehaviour {
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoadScene()
    {
        
        GameObject.Destroy(GameObject.Find("UI_In Game"));
        /*https://forum.unity3d.com/threads/scenemanager-loadscene-additive-and-set-active.380826/ */
    }
}
