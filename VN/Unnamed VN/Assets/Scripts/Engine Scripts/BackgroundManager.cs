using UnityEngine;
using System.Collections;

public class BackgroundManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ChangeBackground("test2");
        GameObject.FindWithTag("ActiveBackground").GetComponent<Background>().MakeTransparent();
    }
	
	// Update is called once per frame
	void Update () {
        GameObject.FindWithTag("ActiveBackground").GetComponent<Background>().FadeIn(3f);
	}
    //changes the current background only a file name is required
    public void ChangeBackground(string s)
    {
        Sprite newBackground = Resources.Load<Sprite>("Backgrounds/" + s);
        if ((newBackground) == null)
        {
            newBackground = Resources.Load<Sprite>("Backgrounds/NoTexture");
            Debug.Log("Fuck");
        }
        /*try
        {
            GameObject.FindWithTag("ActiveBackground").tag = "InactiveBackground";
            GameObject.FindWithTag("InactiveBackground").GetComponent<SpriteRenderer>().color = new Color(255f, 255f, 255f, 0f);
        }
        catch(System.NullReferenceException){}*/
        //GameObject.Instantiate(newBackground);//.tag = "ActiveBackground";
        GameObject.FindWithTag("ActiveBackground").GetComponent<SpriteRenderer>().sprite = newBackground;
    }

}
