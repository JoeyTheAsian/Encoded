using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
    //background music
    public GameObject bgm;
    public GameObject Sounds;
    public Dictionary<string, AudioClip> SoundEffects = new Dictionary<string, AudioClip>();
    public float EffectVolume = .1f;

    AudioClip TextScroll;
    
    // Use this for initialization
    void Start () {
        TextScroll = Resources.Load<AudioClip>("SoundEffects/TextScroll");
        AudioSource audioSource = bgm.GetComponent<AudioSource>();
        audioSource.volume = GameManager.musicVolume;
        AudioClip[] musicList = Resources.LoadAll<AudioClip>("Music");
        foreach(AudioClip ac in musicList) {
            if (!SoundEffects.ContainsKey(ac.name)) {
                SoundEffects.Add(ac.name, ac);
            }
        }
    }

	public void ChangeMusic(string path) {
		ChangeMusic(path, false);
	}

	public void ChangeMusic(string path, bool loop) {
		AudioSource audioSource = bgm.GetComponent<AudioSource>();
        if (!SoundEffects.ContainsKey(path)) {
            SoundEffects.Add(path, Resources.Load<AudioClip>("Music/" + path));
        }
        AudioClip temp;
        SoundEffects.TryGetValue(path, out temp);
        if(audioSource.clip != temp) {
            audioSource.clip = temp;
            audioSource.loop = loop;
            audioSource.Play();
        }
    }
	
	// Update is called once per frame
	void Update () {

	}
    //plays the sound when text animation in the dialogue box is active
    public void PlayTextScroll() {
        Sounds.GetComponent<AudioSource>().PlayOneShot(TextScroll, GameManager.sfxVolume * .7f);
    }
    public void PlaySound(string path) {
        GameObject g = Instantiate<GameObject>(Sounds);
        AudioSource audioSource = g.AddComponent<AudioSource>();
        audioSource.volume = GameManager.sfxVolume;
        if (!SoundEffects.ContainsKey(path)) {
            SoundEffects.Add(path, Resources.Load<AudioClip>("SoundEffects/" + path));
        }
        AudioClip temp;
        SoundEffects.TryGetValue(path ,out temp);
        audioSource.clip = temp;
        audioSource.loop = false;
        audioSource.Play();
        Destroy(g, audioSource.clip.length);
    }
    public void UpdateMusicVolume() {
        AudioSource audioSource = bgm.GetComponent<AudioSource>();
        audioSource.volume = GameManager.musicVolume;
    }
}   
