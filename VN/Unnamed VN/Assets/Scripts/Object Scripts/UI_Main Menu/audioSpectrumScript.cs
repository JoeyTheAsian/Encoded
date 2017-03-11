using UnityEngine;
using System.Collections;

public class audioSpectrumScript : MonoBehaviour {
    public GameObject prefab; //object bar
    public GameObject setParentPanel; //as parent

    public int numberOfObjects = 20;
    public int numberOfSample = 1024;
    public int barGap = 2;
    //public float radius = 5f;
    public GameObject[] bars;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < numberOfObjects; i++)
        {
            //float angle = i * Mathf.PI * 2 / numberOfObjects;

            Vector3 pos = new Vector3(i * 5+barGap,250, 0);

            GameObject setBar = Instantiate(prefab, pos, Quaternion.identity) as GameObject;

            setBar.transform.SetParent(setParentPanel.transform);// Instantiate a prefab as a child of a Canvas
            setBar.transform.localScale = new Vector3(0.1F, 0, 0);
        }
        bars = GameObject.FindGameObjectsWithTag("bars"); //set tag for the object that drop into asset
    }
	
	// Update is called once per frame
	void Update () {
        float[] samples = new float[numberOfSample]; AudioListener.GetSpectrumData(samples, 0, FFTWindow.Hamming);
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 previousScale = bars[i].transform.localScale;
            previousScale.y = Mathf.Lerp(previousScale.y, samples[i] * 40, Time.deltaTime * 30);
            bars[i].transform.localScale = previousScale;
        }
    }
}
