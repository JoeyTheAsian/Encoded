using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    public Menu CurrentMenu; //InGame Menu

    public void Start()
    {
        ShowMenu(CurrentMenu); //set InGame Menu start at begin
    }

    public void ShowMenu(Menu menu) //menu depend on On Click() send
    {
        if (CurrentMenu != null) 
            CurrentMenu.IsOpen = false; //(menu.cs) return value to IsOpen

        CurrentMenu = menu; // menu.cs that trigger animation
        CurrentMenu.IsOpen = true;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("You have enter the ESC key!");
        }
    }
}
