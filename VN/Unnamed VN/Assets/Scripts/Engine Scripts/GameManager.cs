using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static float scrollSpeed = .5f;
    public static float autoDelay = .5f;
    public static float musicVolume = .5f;
    public static float sfxVolume = .5f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetResolution(int index) {
        switch (index) {
            case 0:
                Screen.SetResolution(800, 600, false);
                break;
            case 1:
                Screen.SetResolution(1200, 800, false);
                break;
            case 2:
                Screen.SetResolution(1366, 768, false);
                break;
            case 3:
                Screen.SetResolution(1440, 900, false);
                break;
            case 4:
                Screen.SetResolution(1280, 1024, false);
                break;
            case 5:
                Screen.SetResolution(1680, 1050, false);
                break;
            case 6:
                Screen.SetResolution(1920, 1080, false);
                break;
            case 7:
                Screen.SetResolution(Screen.height, Screen.width, true);
                break;
        }
    }
    public void SetMusicVolume(float vol) {
        musicVolume = vol;
        GameObject.Find("AudioManager").GetComponent<AudioManager>().UpdateMusicVolume();
    }
    public void SetSfxVolume(float vol) {
        sfxVolume = vol;
    }
    public void SetScrollSpeed(float spd) {
        scrollSpeed = spd;
    }
}
