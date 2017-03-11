using UnityEngine;
using System.Collections;

public class Spectrum_Bar : MonoBehaviour {
    
    // Use this for initialization
    public GameObject prefab;
    public int numberOfObjects = 6;
    //public float radius = 5f;
    public float gridX = 5f;
    public float gridY = 5f;
    public float spacing = 2f;
    public GameObject[] bars;

    void Start () {
        /*for (int y = 0; y < gridY; y++)
        {
            for (int x = 0; x < gridX; x++)
            {
                Vector3 pos = new Vector3(x, 0, y) * spacing;
                Instantiate(prefab, pos, Quaternion.identity);
            }
        }*/
        bars= GameObject.FindGameObjectsWithTag("bars");
    }

    // Update is called once per frame
    void Update() {
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        for (int i = 0; i < numberOfObjects; i++)
        {
            Vector3 previousScale = bars[i].transform.localScale;
            previousScale.y = spectrum[i] * 20;
            bars[i].transform.localScale =previousScale;
        }
	}
}

