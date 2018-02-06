using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class isOverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool isOver = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse enter");
        isOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exit");
        isOver = false;
    }
}
