using UnityEngine;
using System.Collections;

public class VisualizerScript : MonoBehaviour {

    // Variables
    public GameObject prefab;
    public GameObject[] bars;
    public int numOfObjects = 20;
    public float radius = 5f;
    public Material mat;
    public GameObject Camerapivot;

    public float r;
    public float g;
    public float b;


    // Use this for initialization
    void Start()
    {

        r = mat.color.r;
        g = mat.color.g;
        b = mat.color.b;

        // Create bars in a circle
        for (int i = 0; i < numOfObjects; i++)
        {
            float angle = i * Mathf.PI * 2 / numOfObjects;
            Vector3 pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            Instantiate(prefab, pos, Quaternion.identity);
        }
        bars = GameObject.FindGameObjectsWithTag("bars");
    }

    // Update is called once per frame
    void Update()
    {
        // Change bar y scale to spectrum data
        float[] spectrum = AudioListener.GetSpectrumData(1024, 0, FFTWindow.Hamming);
        for (int i = 0; i < numOfObjects; i++)
        {
            Vector3 prevScale = bars[i].transform.localScale;
            prevScale.y = Mathf.Lerp(prevScale.y, spectrum[i] * 30, Time.deltaTime * 20);
            bars[i].transform.localScale = prevScale;
        }

        Camerapivot.transform.Rotate(0, Time.deltaTime * 100, 0);

    }

    void OnGUI()
    {

        r = GUI.VerticalSlider(new Rect(140, 10, 30, Screen.height - 40), r, 0.0F, 1.0F);
        g = GUI.VerticalSlider(new Rect(180, 10, 30, Screen.height - 40), g, 0.0F, 1.0F);
        b = GUI.VerticalSlider(new Rect(220, 10, 30, Screen.height - 40), b, 0.0F, 1.0F);

        mat.color = new Color(r, g, b);

        GUI.TextArea(new Rect(240, (Screen.height - 40)/2 +10, 55, 43), "COLOR BARS");
    }
}
