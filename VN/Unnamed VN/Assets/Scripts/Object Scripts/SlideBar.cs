using UnityEngine;
using System.Collections;

public class SlideBar : MonoBehaviour {
    private float timer;
    private float velocity = 300f;
    private float curVelocity = 300f;
    public float acceleration;
    private float start;
    public float target;
    bool animated;
	// Use this for initialization
	void Start () {
        animated = true;
        start = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (animated) {
            if (curVelocity > 0f) {
                    curVelocity -= acceleration * Time.deltaTime;
            } else {
                curVelocity = 0f;
            }
            if (transform.position.x <= target) {
                gameObject.transform.position += new Vector3(curVelocity * Time.deltaTime, 0f, 0f);
            } else {
                animated = false;
            }

        }/* else {
            if (curVelocity > 0f) {
                curVelocity -= acceleration * Time.deltaTime;
            } else {
                curVelocity = 0f;
            }
            if (transform.position.x >= start) {
                gameObject.transform.position += new Vector3(-curVelocity * Time.deltaTime, 0f, 0f);
            } else {
                animated = true;
            }
        }*/
    }
}
