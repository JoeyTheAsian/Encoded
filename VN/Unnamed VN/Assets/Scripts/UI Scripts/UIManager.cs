using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Menu CurrentMenu; //InGame Menu
    public Menu triggerMenu; //menu triggered by ESC
    Stack st = new Stack();
    Menu rootMenu;


    public void Start()
    {
        ShowMenu(CurrentMenu); //set InGame Menu start at begin
        st.Push(CurrentMenu); //Add menu to Stack
        rootMenu = CurrentMenu;
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
        else st.Pop();// remove one stack if going to previous menu
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        //if (Input.GetKeyDown("escape"))
        {
            /*if (previousMenu != null)
                previousMenu.IsOpen = true;*/
            if (CurrentMenu == rootMenu)
            {
                CurrentMenu.IsOpen = false; //close rootMenu
                CurrentMenu = triggerMenu;  //open triggerMenu
                CurrentMenu.IsOpen = true;
                st.Push(CurrentMenu);
            }
            else
            {
                //if currentmenu is not rootmenu, go to previous menu and remove one stack
                CurrentMenu.IsOpen = false;
                CurrentMenu = (Menu)st.Peek();
                CurrentMenu.IsOpen = true;
                st.Pop();
            }
            
            Debug.Log("You have enter the ESC key!");
        }
    }
}
