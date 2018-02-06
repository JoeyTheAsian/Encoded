using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class rotateScript : MonoBehaviour {
    public float speed;
    [SerializeField]
    private Image content;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        centerBox();
	}

    private void centerBox()
    {
        content.transform.Rotate(0, 0,- Time.deltaTime *speed);
    }
}
