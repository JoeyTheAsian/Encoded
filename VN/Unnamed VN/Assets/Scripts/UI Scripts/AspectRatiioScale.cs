using UnityEngine;
using System.Collections;

public class AspectRatiioScale : MonoBehaviour {
    public Vector2 scaleOnRatio1 = new Vector2(0.1f, 0.1f);
    private Transform myTrans;
    private float widthHeightRatio;

    // Use this for initialization
    void Start () {
        myTrans = transform;
        SetScale();
    }

    void SetScale()
    {
        //find the aspect ratio
        widthHeightRatio = (float)Screen.width / Screen.height;

        //Apply the scale. We only calculate y since our aspect ratio is x (width) authoritative: width/height (x/y)
        myTrans.localScale = new Vector3(scaleOnRatio1.x, widthHeightRatio * scaleOnRatio1.y, 1);
    }

    // Update is called once per frame
    void Update () {
        myTrans = transform;
        SetScale();
    }
}
