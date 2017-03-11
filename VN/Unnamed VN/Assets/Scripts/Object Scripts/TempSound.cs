using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!gameObject.GetComponent<AudioSource>().isPlaying) {
            Destroy(gameObject);
        }
	}
}
