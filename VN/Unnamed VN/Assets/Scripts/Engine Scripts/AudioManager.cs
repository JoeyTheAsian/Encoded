using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {
    //background music
    public GameObject bgm;
    public GameObject SoundEffects;

    public float EffectVolume = .1f;

    AudioClip TextScroll;
    
    // Use this for initialization
    void Start () {
        TextScroll = Resources.Load<AudioClip>("SoundEffects/TextScroll");
        //ChangeMusic("Computer Lab Music");
	}

    public void ChangeMusic(string path) {
        bgm.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Music/" + path);
		bgm.GetComponent<AudioSource>().Play(0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //plays the sound when text animation in the dialogue box is active
    public void PlayTextScroll() {
        SoundEffects.GetComponent<AudioSource>().PlayOneShot(TextScroll, EffectVolume);
    }
}
