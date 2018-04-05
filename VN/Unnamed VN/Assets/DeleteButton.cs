using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class DeleteButton : MonoBehaviour {
    public void Delete(Text t) {
        File.Delete(t.text + ".txt");
        GameObject.Find("SaveContent").GetComponent<SaveManager>().Refresh();
    }
}
