using UnityEngine;
using System.Collections;

public class SlideBar : MonoBehaviour {
    public float duration = 3f;
    private float timer;
    private float velocity = 400f;
    private float curVelocity = 400f;
    public float target;
    bool animated;
	// Use this for initialization
	void Start () {
        animated = true;
        timer = duration;
	}
	
	// Update is called once per frame
	void Update () {
        if (animated)
        {
            if (curVelocity > 0f)
            {
                if(timer <= duration) {
                    timer -= Time.deltaTime;
                    curVelocity = velocity * timer / duration;
                }
            }else
            {
                curVelocity = 0f;
            }
            if(transform.position.x <= target) {
                gameObject.transform.position += new Vector3(curVelocity * Time.deltaTime, 0f, 0f);
            }

        }
    }
}
