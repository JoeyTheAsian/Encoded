using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class barScript : MonoBehaviour {
    [SerializeField]
    private float fillAmount;
    [SerializeField]
    private Image Bar1;
    [SerializeField]
    private Image Bar2;
    [SerializeField]
    private Image Bar3;
    [SerializeField]
    private Image Bar4;
    [SerializeField]
    private Image Bar5;
    [SerializeField]
    private Image Bar6;
    [SerializeField]
    private Image Bar7, Bar8, Bar9, Bar10, Bar11, Bar12;

    private Image[] Bar = new Image[12];
    private float[] size = new float[12];
    public int sampleBandNumBegin = 1;
    // Use this for initialization
    void Start () {
        //top
        Bar[0] = Bar1;
        Bar[1] = Bar2;
        Bar[2] = Bar3;
        Bar[3] = Bar4;
        Bar[4] = Bar5;
        Bar[5] = Bar6;

        //bottom
        Bar[6] = Bar7;
        Bar[7] = Bar8;
        Bar[8] = Bar9;
        Bar[9] = Bar10;
        Bar[10] = Bar11;
        Bar[11] = Bar12;

        for (int i = 0; i < 12; i++)
            size[i] = 0;
    }
	
	// Update is called once per frame
	void Update () {
        float[] samples = new float[4096]; AudioListener.GetSpectrumData(samples, 0, FFTWindow.Hamming);
        for (int i = 0; i < 12; i++)
        {
            //int x = 0; if (i > 6) x = 20;
            float previousSize = size[i];
            size[i] = (float)Mathf.Lerp(previousSize, samples[(i+ sampleBandNumBegin) *16] * 40, Time.deltaTime * 30);
            barFillAmount(size[i]*500, Bar[i]);
            //Debug.Log("size=" + size);
            if (i==1)
                Debug.Log("size=" + size[i] * 100);
        }
    }

    private void barFillAmount(float size, Image barN)
    {
        barN.fillAmount = Map(size, 0, 100, 0, 1);
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
