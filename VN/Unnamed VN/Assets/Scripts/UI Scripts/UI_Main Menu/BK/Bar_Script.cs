using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Bar_Script : MonoBehaviour {

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;
    [SerializeField]
    private Image contentclone;

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

    private Image[] Bar = new Image[6];

    private float size;
    public GameObject prefab;
    public GameObject barPanel;

    public int numberOfObjects = 20;
    public float radius = 5f;
    public GameObject[] bars;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < numberOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfObjects;
            //Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            //Vector3 pos = new Vector3(700+i*30, 500, 0);
            Vector3 pos = new Vector3(i * 30, 500, 0);
            //GameObject setBar = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            GameObject setBar = Instantiate(prefab, pos, Quaternion.identity) as GameObject;
            //instantiatedPrefab.transform.localScale = Vector3(1, 2, 3);
            setBar.transform.SetParent(barPanel.transform);// Instantiate a prefab as a child of a Canvas
            setBar.transform.localScale = new Vector3(0.1F, 0, 0);
        }
        bars = GameObject.FindGameObjectsWithTag("bars");


        for (int i = 0; i < 6; i++)
        {
            //Instantiate(contentclone, barPanel.transform);
           
}
       Bar[0] = Bar1;
        Bar[1] = Bar2;
        Bar[2] = Bar3;
        Bar[3] = Bar4;
        Bar[4] = Bar5;
        Bar[5] = Bar6;
    }

    // Update is called once per frame
    void Update() {
        float[] samples = new float[1024]; AudioListener.GetSpectrumData(samples, 0, FFTWindow.Hamming);
        //float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        //for (int i = 1; i < 2; i++)
          //  size = (int)Mathf.Log(samples[256] ) * -1;
        /*
        float[] spectrum = new float[512];
        
        AudioListener.GetSpectrumData(spectrum, 0, FFTWindow.Rectangular);

        for (int i = 1; i < spectrum.Length - 1; i++)
        {
            Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
            Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
            Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.blue);
        }*/
        

        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 previousScale = bars[i].transform.localScale;
            //previousScale.y = Mathf.Lerp(previousScale.y, samples[i] * 20, Time.deltaTime * 30);
            previousScale.y = Mathf.Lerp(previousScale.y, samples[i] * 40, Time.deltaTime * 30);
            //bars[i].transform.localScale = previousScale;
            bars[i].transform.localScale = previousScale;
            if (i==13)
                //Debug.Log("bar 13 size=" + samples[i] * 20);
            if (i == 13)
                //size = (int)Mathf.Log(samples[i]) * -10;
                size = (float)Mathf.Lerp(previousScale.y, samples[i] * 40, Time.deltaTime * 30) * 100;
            //size = 50;
            //Debug.Log("size=" + size);
        }
        
        for (int i = 0; i < 6; i++)
        {
            Vector3 previousScale = bars[i].transform.localScale;
            previousScale.y = Mathf.Lerp(previousScale.y, samples[i*1024/6] * 40, Time.deltaTime * 30);

            size = (float)Mathf.Lerp(previousScale.y, samples[i] * 40, Time.deltaTime * 30) * 100 *5;
            barFillAmount(size, Bar[i]);
            Debug.Log("size=" + size);
        }

        
        HandleBar();

    }

    private void barFillAmount(float size, Image barN)
    {
        barN.fillAmount = Map(size, 0, 100, 0, 1);
    }
    private void HandleBar()
    {
        //content.fillAmount = fillAmount;
        content.fillAmount = Map(size,0,100,0,1);
        //Debug.Log("size=" + size);
    }

    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
