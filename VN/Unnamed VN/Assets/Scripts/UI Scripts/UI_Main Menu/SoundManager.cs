using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
    void Awake()
    {
    }


    // Update is called once per frame
    void Update () {
        if (!(GetComponent<AudioSource>().isPlaying))
        {
            GetComponent<AudioSource>().Play();
        }
        else
        {
            //Debug.log("Something is wrong with Music.");
        }
    }
}
