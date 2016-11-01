using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueBox : MonoBehaviour {
    public float PercentageMargin = .02f;
    public float Opacity = .75f;
	// Use this for initialization
	void Start () {
        AutoSize();
        Color boxColor = gameObject.GetComponent<Image>().color;
        boxColor = new Color(boxColor.r, boxColor.g, boxColor.b, Opacity);
        gameObject.GetComponent<Image>().color = boxColor;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    //automatically scales the UI texture
    void AutoSize()
    {
        //85% of screen width and 35% of screen height
        //right & top margin
        //gameObject.transform.localScale = new Vector3(.95f, 1f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        int width = Camera.main.pixelWidth;
        int height = Camera.main.pixelHeight;
        rectTransform.offsetMax = new Vector2(width * - PercentageMargin, height * .65f);
        rectTransform.offsetMin = new Vector2(width * PercentageMargin, width * PercentageMargin);

    }
}
