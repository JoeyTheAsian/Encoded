using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Menu CurrentMenu; //InGame Menu
    public Menu previousMenu; //menu triggered by ESC
    Stack st = new Stack();

    public void Start()
    {
        ShowMenu(CurrentMenu); //set InGame Menu start at begin
        st.Push(CurrentMenu); //Add menu to Stack
    }

    public void ShowMenu(Menu menu) //menu depend on On Click() send
    {
        //close current menu, open menu depend on On Click()
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false; //(menu.cs) return value to IsOpen

        CurrentMenu = menu; // menu.cs that trigger animation
        CurrentMenu.IsOpen = true;

        if (menu != (Menu)st.Peek()) //For initial, if new menu is not the previous menu
            //then 

            /*
             * 1
             * 2
             * 3
             */
            //st.Push(CurrentMenu);
            st.Push(menu);
        //else st.
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
