using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System;

// Define the functions which can be called from the .dll.
internal static class OpenCVInterop
{
    [DllImport("AssignmentTwo")]
    internal static extern int Init(ref int outCameraWidth, ref int outCameraHeight);

    [DllImport("AssignmentTwo")]
    internal unsafe static extern void Detect(CvCircle* detectedObjects, int xNew, int yNew);

    [DllImport("AssignmentTwo")]
    internal static extern int Close();
}

// Define the structure to be sequential and with the correct byte size (3 ints = 4 bytes * 3 = 12 bytes)
[StructLayout(LayoutKind.Sequential, Size = 32)]
public struct CvCircle
{
    public int X, Y, Red, Green, Blue, nRed, nGreen, nBlue;
}

public class OpenCVObjectDetection : MonoBehaviour
{
    public static List<Vector2> NormalizedObjectPosition { get; private set; }
    public static List<Vector3> NormalizedColorValue { get; private set; }
    public static List<Vector3> NormalizedNewColorValue { get; private set; }
    public static Vector2 CameraResolution;

    // Downscale factor to speed up detection
    private const int DetectionDownScale = 1;
    private bool _ready;
    private int _maxObjectDetectCount = 5;
    int accelerationValueX = 1;
    int accelerationValueY = 1;
    int accelerationValueZ = 1;
    private CvCircle[] _objects;
    public Text myText;

    private double fx = 6.4423573350900188e+02;
    private double fy = 6.4423573350900188e+02;
    private double cx = 320;
    private double cy = 240;




    public Vector3 accelerationArray;
    public Vector3 newPosition, orgPosition, diffPos, accelerometerInput, displacement, displacementUnscaled;
    public Vector3 lastFrameAcceleration, lastFrameVelocityTime;
    public float acceleroThresh = 0.09f;

    void Start()
    {
        Input.gyro.enabled = true;
        accelerationArray = Vector3.zero;
        displacement = Vector3.zero;
        lastFrameAcceleration = Vector3.zero;
        lastFrameVelocityTime = Vector3.zero;

        newPosition = Vector3.zero;
        orgPosition = Vector3.zero;



        int camWidth = 0, camHeight = 0;
        int result = OpenCVInterop.Init(ref camWidth, ref camHeight);
        Debug.Log("start");
        if (result < 0)
        {
            if (result == -1)
            {
                Debug.LogWarningFormat("[{0}] Failed to find cascades definition.", GetType());
            }
            else if (result == -2)
            {
                Debug.LogWarningFormat("[{0}] Failed to open camera stream.", GetType());
            }
            return;
        }

        CameraResolution = new Vector2(camWidth, camHeight);
        _objects = new CvCircle[_maxObjectDetectCount];
        NormalizedObjectPosition = new List<Vector2>();
        NormalizedColorValue = new List<Vector3>();
        NormalizedNewColorValue = new List<Vector3>();

        _ready = true;
    }

    void Update()
    {
        if (!_ready)
            return;

        accelerometerInput = Input.acceleration;
        if(Mathf.Abs(Input.acceleration[0])>acceleroThresh)
        {
            accelerationArray = (Input.acceleration - Input.gyro.gravity);
            //print("accelerationArray.x : "+accelerationArray.x);
        }
        else
        {
            accelerationArray = Vector3.zero;
            newPosition = Vector3.Lerp(newPosition, orgPosition,0.5f);
        }
        //print("accelerationArray.x : " + accelerationArray.x);
        accelerationArray[1] = 0.0f;
        accelerationArray[2] = 0.0f;
        Vector3 temp = Vector3.zero;
        if (accelerationArray[0] > 0.0001f)
            temp = accelerationArray;

        displacementUnscaled = newPosition - orgPosition + (temp * Time.deltaTime * Time.deltaTime);
        diffPos = newPosition - orgPosition;

        displacement = displacementUnscaled * 1f;
        orgPosition = newPosition;
        newPosition = newPosition + displacement;
        myText.text = "New Position: " + newPosition + "\n";
        Debug.Log("wowww : " + newPosition);

        accelerationValueX = Math.Abs((int)(Input.acceleration.x * 10));
        accelerationValueY = Math.Abs((int)(Input.acceleration.y  * 10));
        accelerationValueZ = 1;
        int xNew = (int)(((newPosition[0] * fx )+ (cx * accelerationValueZ) )/ accelerationValueZ);
        int yNew = (int)(((newPosition[1] * fy )+ (cy * accelerationValueZ) )/ accelerationValueZ);
        myText.text = "xNew: " + xNew + "\n yNew: "+ yNew;
        //Debug.Log("xNew :: " + xNew +","+ accelerationValueX + "\t yNew :: " + yNew+","+ accelerationValueY);
        xNew = 0;
        yNew = 0;

        

        unsafe
        {
            fixed (CvCircle* detectedObjects = _objects)
            {
                OpenCVInterop.Detect(detectedObjects, xNew / 5, yNew / 17);
            }
        }

        NormalizedObjectPosition.Clear();
        NormalizedColorValue.Clear();
        NormalizedNewColorValue.Clear();
        NormalizedObjectPosition.Add(new Vector2((_objects[0].X * DetectionDownScale) / CameraResolution.x, 1f - ((_objects[0].Y * DetectionDownScale) / CameraResolution.y)));
        NormalizedColorValue.Add(new Vector3(_objects[0].Red, _objects[0].Green, _objects[0].Blue));
        NormalizedNewColorValue.Add(new Vector3(_objects[0].nRed, _objects[0].nGreen, _objects[0].nBlue));

        Debug.Log("centerX ::: " + _objects[0].X + "\tcenterY ::: " + _objects[0].Y);
        //Debug.Log("newasd" + NormalizedNewColorValue[0].x);
    }

    void OnApplicationQuit()
    {
        if (_ready)
        {
            Debug.Log("Application ending after " + Time.time + " seconds");
            OpenCVInterop.Close();
        }
   }

}
