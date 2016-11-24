using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
    public Menu CurrentMenu; //InGame Menu
    public Menu triggerMenu; //menu triggered by ESC
    Menu rootMenu;
    Stack st = new Stack();
    

    public void Start()
    {
        rootMenu = CurrentMenu;
        //if (rootMenu == CurrentMenu)
        //    Debug.Log("(rootMenu == CurrentMenu)! " + rootMenu + " and CurrentMenu is " + CurrentMenu);
        //else
        //    Debug.Log("(rootMenu != CurrentMenu)! " + rootMenu + " and CurrentMenu is " + CurrentMenu);
        st.Push(CurrentMenu); //Add menu to Stack
        //Debug.Log("st.Push(CurrentMenu)! " + st.Peek());
        ShowMenu(CurrentMenu); //set InGame Menu start at begin
        Debug.Log("st.Peek()! " + st.Peek());


    }

    public void ShowMenu(Menu menu) //menu depend on On Click() send
    {
        //close current menu, open menu depend on On Click()
        if (CurrentMenu != null) // close current menu
            CurrentMenu.IsOpen = false; //(menu.cs) return value to IsOpen



        /**/
        if (CurrentMenu == rootMenu)
        {

            //st.Push(menu);
            //Debug.Log("st.Push(menu)! " + menu);
            //Debug.Log("st.Peek()! " + st.Peek());
        }
        else if (menu != (Menu)st.Peek()) //For initial, if new menu is not the previous menu
        {
            if (menu == triggerMenu)
            {
                //st.Pop();// remove one stack if going to previous menu


                //Debug.Log("st.Pop()! " + menu);
                //Debug.Log("st.Peek()! " + st.Peek());
            }
            else st.Push(CurrentMenu);
        }
        else if (menu != rootMenu) st.Pop();
            

        CurrentMenu = menu; // menu.cs that trigger animation
        CurrentMenu.IsOpen = true;

        Debug.Log("CurrentMenu is " + CurrentMenu + "st.Peek()! " + st.Peek());
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        //if (Input.GetKeyDown("escape"))
        {
            //rootMenu = CurrentMenu;
            //if (rootMenu == CurrentMenu)
            //    Debug.Log("(rootMenu == CurrentMenu)! rootMenu is " + rootMenu + " and CurrentMenu is " + CurrentMenu);
            //else
            //    Debug.Log("(rootMenu != CurrentMenu)! rootMenu is " + rootMenu + " and CurrentMenu is " + CurrentMenu);

            if (CurrentMenu == rootMenu)
            {
                CurrentMenu.IsOpen = false; //close rootMenu
                CurrentMenu = triggerMenu;  //open triggerMenu
                //triggerMenu.IsOpen = true;
                CurrentMenu.IsOpen = true;
                //st.Push(CurrentMenu);
                Debug.Log("(CurrentMenu == rootMenu)!");
            }
            else if (CurrentMenu == triggerMenu)
            {
                CurrentMenu.IsOpen = false;
                CurrentMenu = (Menu)st.Peek();
                CurrentMenu.IsOpen = true;
                Debug.Log("(CurrentMenu == triggerMenu)!");
            }
            else //CurrentMenu != rootMenu, also != triggerMenu
            {
                //if currentmenu is not rootmenu, go to previous menu and remove one stack
                CurrentMenu.IsOpen = false;
                CurrentMenu = (Menu)st.Peek();
                //Debug.Log("You have enter the ESC key! And the value of (Menu)st.Peek() is " + (Menu)st.Peek() 
                  //  + " and the value of CurrentMenu is " + CurrentMenu);
                CurrentMenu.IsOpen = true;
                st.Pop();
                Debug.Log("(CurrentMenu != rootMenu)!");
            }
            
            Debug.Log("You have enter the ESC key!");
        }
    }
}
