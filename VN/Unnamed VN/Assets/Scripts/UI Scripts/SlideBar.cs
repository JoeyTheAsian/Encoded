using UnityEngine;
using System.Collections;

public class SlideBar : MonoBehaviour {
    public float velocity = 550f;
    public float target;
    bool animated;
	// Use this for initialization
	void Start () {
        animated = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (animated)
        {
            if (velocity > 0f)
            {
                velocity -= Time.deltaTime * 150f;
            }else
            {
                velocity = 0f;
            }
            if (gameObject.transform.position.x < target)
            {
                gameObject.transform.position += new Vector3(velocity * Time.deltaTime, 0f, 0f);
            }else
            {
                gameObject.transform.position = new Vector3(target, gameObject.transform.position.y, gameObject.transform.position.z);
                animated = false;
            }
        }
    }
}
