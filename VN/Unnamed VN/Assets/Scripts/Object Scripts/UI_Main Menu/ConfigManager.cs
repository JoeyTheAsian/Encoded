using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour {
    public configScript ConfigMenu;

    // Use this for initialization
    void Start () {
        //ShowMenu(ConfigMenu);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowMenu(configScript menu)
    {
        if (ConfigMenu != null)
            ConfigMenu.IsOpen = false;

        ConfigMenu = menu;
        ConfigMenu.IsOpen = true;
    }

    public void CloseMenu(configScript menu)
    {
        if (ConfigMenu != null)
            ConfigMenu.IsOpen = false;

        ConfigMenu = menu;
        ConfigMenu.IsOpen = false;
    }
}
