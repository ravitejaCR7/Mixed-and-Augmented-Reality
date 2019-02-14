using UnityEngine;
public class AccessAccelerometer : MonoBehaviour
{
    //public float speedInX, speedInZ;
    // Use this for initialization

    public new Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (OpenCVObjectDetection.NormalizedColorValue.Count == 0)
            return;
        Color color123 = new Color32((byte)(OpenCVObjectDetection.NormalizedColorValue[0].x), (byte)(OpenCVObjectDetection.NormalizedColorValue[0].y), (byte)(OpenCVObjectDetection.NormalizedColorValue[0].z), 255);
        renderer.material.color = color123;
        
    }
}
