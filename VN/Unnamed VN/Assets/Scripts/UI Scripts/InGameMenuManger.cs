using UnityEngine;
using System.Collections;

public class InGameMenuManger : MonoBehaviour {
    public Menu CurrentMenu; //InGame Menu
    public Menu MainMenu; //menu triggered by ESC

    public void Start()
    {
        //ShowMenu(CurrentMenu); //set InGame Menu start at begin
    }

    public void ShowMenu(Menu menu) //menu depend on On Click() send
    {
        //close current menu, open menu depend on On Click()
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false; //(menu.cs) return value to IsOpen

        CurrentMenu = menu; // menu.cs that trigger animation
        CurrentMenu.IsOpen = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        //if (Input.GetKeyDown("escape"))
        {
            MainMenu.IsOpen = true;
            CurrentMenu.IsOpen = false;
            Debug.Log("You have enter the ESC key!");
        }
    }
}
