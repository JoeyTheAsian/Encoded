using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class SaveManager : MonoBehaviour
{
    public Scripting scripting;
    public GameObject saveDataUI;
    // Use this for initialization
    void Start()
    {
        Refresh();
    }
    public void Refresh()
    {
        for(int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i));
        }
        //due to this implementation, don't include other .txt files in the game directory
        Debug.LogError("Refresh");
        //loop through all files and display in panel
        foreach (string file in System.IO.Directory.GetFileSystemEntries(Directory.GetCurrentDirectory()))
        {
            string fileName = Path.GetFileName(file);
            if (fileName.Contains(".txt") && !fileName.Contains("liveSave.txt") && !fileName.Contains("Script.txt"))
            {
                Debug.LogError("Loaded: " + fileName);
                GameObject newSaveInfo = Instantiate(saveDataUI, gameObject.transform);
                newSaveInfo.transform.SetParent(gameObject.transform);

                //get date & time data from save file
                string[] data = File.ReadAllLines(fileName);
                for(int j = 0; j < data.Length; j++)
                {
                    Debug.LogWarning(data[j]);
                }

                string lastButOne = data[data.Length - 2];
                //get save file name
                string strippedName = fileName.Remove(fileName.Length - 4);
                //set text
                newSaveInfo.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>().text = strippedName + "\n" + lastButOne;
                Debug.Log(strippedName + "\n" + lastButOne);
            }
        }
    }
}
