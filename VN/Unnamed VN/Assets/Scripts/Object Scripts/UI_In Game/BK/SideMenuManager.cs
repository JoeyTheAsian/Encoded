using UnityEngine;
using System.Collections;
using UnityEngine.UI; //Button

public class MainMenuManager : MonoBehaviour {
    public Button SaveButton;
    public Button LoadButton;
    public Button OptionButton;
    public Button ExitButton;
    public Button yourButton;
    // Use this for initialization
    void Start () {
        Button SaveBtn = SaveButton.GetComponent<Button>();
        SaveBtn.onClick.AddListener(SaveOnClick);

        Button LoadBtn = LoadButton.GetComponent<Button>();
        LoadBtn.onClick.AddListener(LoadOnClick);

        Button OptionBtn = OptionButton.GetComponent<Button>();
        OptionBtn.onClick.AddListener(OptionOnClick);

        Button ExitBtn = ExitButton.GetComponent<Button>();
        ExitBtn.onClick.AddListener(ExitOnClick);

        //Button btn = yourButton.GetComponent<Button>();
        //btn.onClick.AddListener(TaskOnClick);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void SaveOnClick()
    {
        Debug.Log("You have clicked the Save Button!");
    }
    void LoadOnClick()
    {
        Debug.Log("You have clicked the Load Button!");
    }
    void OptionOnClick()
    {
        Debug.Log("You have clicked the Option Button!");
    }

    void ExitOnClick()
    {
        Application.Quit();
        Debug.Log("You have clicked the Exit Button!");
    }

    /*void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
    }*/
}
