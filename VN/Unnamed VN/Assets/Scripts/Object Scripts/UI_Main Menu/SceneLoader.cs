using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    AsyncOperation async = new AsyncOperation();
    AsyncOperation scene = new AsyncOperation();
    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator Load(string sceneName, string UI) {
        scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        scene.allowSceneActivation = false;
       // async = SceneManager.LoadSceneAsync(UI, LoadSceneMode.Additive);
        //async.allowSceneActivation = false;
        yield return 0;
    }
    public void LoadScene(string sceneName)
    {
        //Application.LoadLevel("Scripting branched");
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive);
        string nextSceneName = "UI_In Game";
        /*AsyncOperation scene = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        async = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        async.allowSceneActivation = false;*/
        //SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        //SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        StartCoroutine(Load(sceneName, nextSceneName));

        scene.allowSceneActivation = true;
       // async.allowSceneActivation = true;

        Destroy(this);
        /*https://forum.unity3d.com/threads/scenemanager-loadscene-additive-and-set-active.380826/ */
    }
    public void LoadSceneMainMenu2(string sceneName)
    {
        async.allowSceneActivation = false;
        SceneManager.LoadScene(sceneName);

    }
}
