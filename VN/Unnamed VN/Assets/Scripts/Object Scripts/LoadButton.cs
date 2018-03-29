using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LoadButton : MonoBehaviour {

    public void Load(Text input) {

        GameObject.Find("Scripting").GetComponent<Scripting>().Load(input);
    }
}
