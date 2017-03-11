using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class centerScript : MonoBehaviour {
    //this script will change the scale of the object when the mouse on over it

    public float fadeTime;
    public bool displayInfo;

    [SerializeField]
    private Image content;

    // Use this for initialization
    void Start () {
        //Screen.showCursor = false;
        //Screen.lockCursor = true;
    }

    // Update is called once per frame
    void Update () {
        //if (displayInfo == true)
        centerCircle(displayInfo);
    }

    void OnMouseOver()
    {
        displayInfo = true;

    }



    void OnMouseExit()

    {
        displayInfo = false;

    }

    private void centerCircle(bool displayInfo)
    {
        Vector3 oldScale = content.transform.localScale;
        Vector3 newScale = content.transform.localScale;

        if (displayInfo == false)
        {
            oldScale.x = Mathf.Lerp(newScale.x, .64f, fadeTime * Time.deltaTime);
            oldScale.y = Mathf.Lerp(newScale.y, .64f, fadeTime * Time.deltaTime);
            content.transform.localScale = oldScale;
        }
        

        if (displayInfo == true)
        {
            newScale.x = Mathf.Lerp(newScale.x, .7f, fadeTime * Time.deltaTime);
            newScale.y = Mathf.Lerp(newScale.y, .7f, fadeTime * Time.deltaTime);
            content.transform.localScale = newScale;
        }
            
    }
}
