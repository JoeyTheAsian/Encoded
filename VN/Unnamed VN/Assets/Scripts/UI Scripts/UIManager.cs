using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Menu CurrentMenu; //InGame Menu
    public Menu previousMenu; //menu triggered by ESC

    public void Start()
    {
        ShowMenu(CurrentMenu); //set InGame Menu start at begin
    }

    public void ShowMenu(Menu menu) //menu depend on On Click() send
    {
        //close current menu, open menu depend on On Click()
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false; //(menu.cs) return value to IsOpen
        
            previousMenu = CurrentMenu;
        CurrentMenu = menu; // menu.cs that trigger animation
        CurrentMenu.IsOpen = true;
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        //if (Input.GetKeyDown("escape"))
        {
            if (previousMenu != null)
                previousMenu.IsOpen = true;
            CurrentMenu.IsOpen = false;
            Debug.Log("You have enter the ESC key!");
        }
    }
}
