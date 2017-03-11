using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
    //background music
    public GameObject bgm;
    public GameObject Sounds;
    public Dictionary<string, AudioClip> SoundEffects;
    public float EffectVolume = .1f;

    AudioClip TextScroll;
    
    // Use this for initialization
    void Start () {
        TextScroll = Resources.Load<AudioClip>("SoundEffects/TextScroll");
	}

	public void ChangeMusic(string path) {
		ChangeMusic(path, false);
	}

	public void ChangeMusic(string path, bool loop) {
		AudioSource audioSource = bgm.GetComponent<AudioSource>();
		audioSource.clip = Resources.Load<AudioClip>("Music/" + path);
		audioSource.loop = loop;
		audioSource.Play();
    }
	
	// Update is called once per frame
	void Update () {

	}
    //plays the sound when text animation in the dialogue box is active
    public void PlayTextScroll() {
        Sounds.GetComponent<AudioSource>().PlayOneShot(TextScroll, EffectVolume);
    }
    public void PlaySound(string path) {
        GameObject g = Instantiate<GameObject>(Sounds);
        AudioSource audioSource = g.AddComponent<AudioSource>();
        audioSource.clip = Resources.Load<AudioClip>("Music/" + path);
        audioSource.loop = false;
        audioSource.Play();
        g.AddComponent<TempSound>();
    }
}   
