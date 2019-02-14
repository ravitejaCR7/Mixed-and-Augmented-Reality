using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereScript : MonoBehaviour {

    public  Renderer renderer1;
    void Start()
    {
        renderer1 = GetComponent<Renderer>();
    }


    // Update is called once per frame
    void Update() {
        
        Color color123 = new Color32((byte)(OpenCVObjectDetection.NormalizedNewColorValue[0].x), (byte)(OpenCVObjectDetection.NormalizedNewColorValue[0].y), (byte)(OpenCVObjectDetection.NormalizedNewColorValue[0].z), 255);
        renderer1.material.color = color123;
        
    }
}
