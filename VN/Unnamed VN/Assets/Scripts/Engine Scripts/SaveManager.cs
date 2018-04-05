using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class SaveManager : MonoBehaviour
{
    public Scripting scripting;
    public GameObject saveDataUI;
    public GameObject panel;
    // Use this for initialization
    void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        int saveNum = 0;
        for(int i = 0; i < panel.transform.childCount; i++){
            Destroy(panel.transform.GetChild(i).gameObject);
        }
        //due to this implementation, don't include other .txt files in the game directory
       // Debug.LogError("Refresh");
        //loop through all files and display in panel
        foreach (string file in System.IO.Directory.GetFileSystemEntries(Directory.GetCurrentDirectory()))
        {
            saveNum++;
            string fileName = Path.GetFileName(file);
            if (fileName.Contains(".txt") && !fileName.Contains("liveSave.txt") && !fileName.Contains("Script.txt"))
            {
                //Debug.LogError("Loaded: " + fileName);
                GameObject newSaveInfo = Instantiate(saveDataUI, panel.transform);
                newSaveInfo.transform.SetParent(panel.transform);

                //get date & time data from save file
                string[] data = File.ReadAllLines(fileName);
                /*for(int j = 0; j < data.Length; j++)
                {
                    Debug.LogWarning(data[j]);
                }*/

                string lastButOne = data[data.Length - 2];
                //get save file name
                string strippedName = fileName.Remove(fileName.Length - 4);
                //set text
                newSaveInfo.transform.GetChild(2).GetComponent<Text>().text = strippedName + "\n" + lastButOne;
                newSaveInfo.transform.GetChild(3).GetComponent<Text>().text = strippedName;
                //Debug.Log(strippedName + "\n" + lastButOne);
            }
        }
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, saveNum * 50);
    }

    public void Hide()
    {
        transform.parent.transform.parent.gameObject.SetActive(false);
    }
    public void Show()
    {
        transform.parent.transform.parent.gameObject.SetActive(true);
    }
}
