using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    //background music
    public GameObject bgm;

    // Use this for initialization
    void Start () {
        ChangeMusic("Computer Lab Music");
        bgm.GetComponent<AudioSource>().Play(0);
	}

    void ChangeMusic(string path) {
        bgm.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/" + path);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
