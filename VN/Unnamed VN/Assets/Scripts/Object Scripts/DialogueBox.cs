using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using System.Collections;

public class DialogueBox : MonoBehaviour {
    public float PercentageMargin = .07f;
    public float PercentageMargin1 = .07f;
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
       AutoSize();
	}

    //automatically scales the UI texture
    void AutoSize()
    {
        //85% of screen width and 35% of screen height
        //right & top margin
        //gameObject.transform.localScale = new Vector3(.95f, 1f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        float width = GameObject.Find("DialogueContainer").GetComponent<RectTransform>().rect.width;
        float height = GameObject.Find("DialogueContainer").GetComponent<RectTransform>().rect.height;
        rectTransform.offsetMax = new Vector2(height * - PercentageMargin1, height * .38f);
        rectTransform.offsetMin = new Vector2(height * PercentageMargin1, height * PercentageMargin);
    }
    public bool isClicked()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
