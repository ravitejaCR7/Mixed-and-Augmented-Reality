using UnityEngine;
using UnityEngine.UI;   
using GoogleARCore;
using System.Collections.Generic;

public class InstantiateObjectOnTouch : MonoBehaviour {

    public GameObject placedObject;

    private Vector3 initalCarPosition;
    private Vector3 finalCarPosition;

    /// <summary>
    /// The first-person camera being used to render the passthrough camera.
    /// </summary>
    public Camera FirstPersonCamera;

    /// <summary>
    /// The gameobject to place when tapping the screen.
    /// </summary>
    public GameObject PlaceGameObjectCar;

    //for single click
    bool firstCarFlag = false;

    //Joystick
    public Joystick joystick;
    private List<float> lastRotations = new List<float>();
    public bool flipRot = true;
    private float horizontal;
    private float vertical;
    public static float angle;

    //acceleration
    float maxSpeed = 6f;
    float timeZeroToMax = 2.5f;
    float timeMaxToZero = 6f;
    float timebreakToZero = 1f;
    float accelRatePerSecond;
    float decelRatePerSecond;
    float brakePerSecond;
    float forwardVelocity;
    Rigidbody rb;

    //Vector 3
    Vector3 myVector = new Vector3(1f, 0.5f, 0.0f);

    //UI Text
    public Text flagText;
    public Text contentText;
    public Text networkText;

    private void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            lastRotations.Add(0f);
        }

        accelRatePerSecond = maxSpeed / timeZeroToMax;
        decelRatePerSecond = -maxSpeed / timeMaxToZero;
        brakePerSecond = -maxSpeed / timebreakToZero;

        forwardVelocity = 0f;

        rb = GetComponent<Rigidbody>();

    }


    // Update is called once per frame
    void Update()
    {
        // Get the touch position from Unity to see if we have at least one touch event currently active
        Touch touch;
        if (Input.touchCount < 1 || (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }
        

        
        Debug.Log("flag is initially f : "+ firstCarFlag);
        flagText.text = firstCarFlag.ToString();

        // Now that we know that we have an active touch point, do a raycast to see if it hits
        // a plane where we can instantiate the object on.
        TrackableHit hit;
        var raycastFilter = TrackableHitFlags.PlaneWithinBounds | TrackableHitFlags.PlaneWithinPolygon;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit) && PlaceGameObjectCar != null)
        {
            if (!firstCarFlag)
            {
                // Create an anchor to allow ARCore to track the hitpoint as understanding of the physical
                // world evolves.
                var anchor = hit.Trackable.CreateAnchor(hit.Pose);

                initalCarPosition = hit.Pose.position;

                // Intanstiate a game object as a child of the anchor; its transform will now benefit
                // from the anchor's tracking.
                placedObject = Instantiate(PlaceGameObjectCar, hit.Pose.position, hit.Pose.rotation);


                // Game object should look at the camera but still be flush with the plane.
                if ((hit.Flags & TrackableHitFlags.PlaneWithinPolygon) != TrackableHitFlags.None)
                {
                    // Get the camera position and match the y-component with the hit position.
                    Vector3 cameraPositionSameY = FirstPersonCamera.transform.position;
                    cameraPositionSameY.y = hit.Pose.position.y;

                    // Have game object look toward the camera respecting his "up" perspective, which may be from ceiling.
                    placedObject.transform.LookAt(cameraPositionSameY, placedObject.transform.up);


                    firstCarFlag = true;                   

                    Debug.Log("flag is set to t : " + firstCarFlag);
                    flagText.text = firstCarFlag.ToString()+" inside";
                }
                     
                // Make the newly placed object a child of the parent
                placedObject.transform.parent = anchor.transform;
            }
        }

    }

    private void FixedUpdate()
    {
        if (firstCarFlag)
        {
            Debug.Log("not working here : " + firstCarFlag);
            flagText.text = firstCarFlag.ToString() + "outside";
            contentText.text = joystick.Horizontal.ToString() + " /// " + joystick.Vertical.ToString();
            //networkText.text = placedObject.transform.position.ToString();

            horizontal = joystick.Direction.x;
            vertical = joystick.Direction.y;
            angle = Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg;
            angle = flipRot ? -angle : angle;

            lastRotations.Add(angle);
            while (lastRotations.Count > 16)
            {
                lastRotations.RemoveAt(0);
            }

            float rotA = lastRotations[0];
            float rotB = lastRotations[1];
            float rotC = lastRotations[2];
            float rotD = lastRotations[3];
            float rotE = lastRotations[4];
            float rotF = lastRotations[5];
            float rotG = lastRotations[6];
            float rotH = lastRotations[7];
            float rotI = lastRotations[8];
            float rotJ = lastRotations[9];
            float rotK = lastRotations[10];
            float rotL = lastRotations[11];
            float rotM = lastRotations[12];
            float rotN = lastRotations[13];
            float rotO = lastRotations[14];
            float rotP = lastRotations[15];

            float rotAB = Mathf.LerpAngle(rotA, rotB, 0.5f);
            float rotCD = Mathf.LerpAngle(rotC, rotD, 0.5f);
            float rotEF = Mathf.LerpAngle(rotE, rotF, 0.5f);
            float rotGH = Mathf.LerpAngle(rotG, rotH, 0.5f);
            float rotIJ = Mathf.LerpAngle(rotI, rotJ, 0.5f);
            float rotKL = Mathf.LerpAngle(rotK, rotL, 0.5f);
            float rotMN = Mathf.LerpAngle(rotM, rotN, 0.5f);
            float rotOP = Mathf.LerpAngle(rotO, rotP, 0.5f);

            float rotABCD = Mathf.LerpAngle(rotAB, rotCD, 0.5f);
            float rotEFGH = Mathf.LerpAngle(rotEF, rotGH, 0.5f);
            float rotIJKL = Mathf.LerpAngle(rotIJ, rotKL, 0.5f);
            float rotMNOP = Mathf.LerpAngle(rotMN, rotOP, 0.5f);

            float rotABCDEFGH = Mathf.LerpAngle(rotABCD, rotEFGH, 0.5f);
            float rotIJKLMNOP = Mathf.LerpAngle(rotIJKL, rotMNOP, 0.5f);

            float rotABCDEFGHIJKLMNOP = Mathf.LerpAngle(rotABCDEFGH, rotIJKLMNOP, 0.5f);

            placedObject.transform.rotation = Quaternion.Euler(new Vector3(0, rotABCDEFGHIJKLMNOP, 0));


            //if (joystick.Direction.y > 0)
            //{
            //    //accelerometer
            //    forwardVelocity += accelRatePerSecond * Time.deltaTime;
            //    forwardVelocity = Mathf.Min(forwardVelocity, maxSpeed);

            //    rb.velocity = transform.forward * forwardVelocity;
            //}
            //if (joystick.Direction.y < 0)
            //{
            //    //decelerate
            //    forwardVelocity += brakePerSecond * Time.deltaTime;
            //    forwardVelocity = Mathf.Max(forwardVelocity, 0);

            //    rb.velocity = transform.forward * forwardVelocity;
            //}
            //if (joystick.Direction.y == 0 && joystick.Direction.x == 0)
            //{
            //    //smooth slowdown
            //    forwardVelocity += decelRatePerSecond * Time.deltaTime;
            //    forwardVelocity = Mathf.Max(forwardVelocity, 0);

            //    rb.velocity = transform.forward * forwardVelocity;

            //}




            //if (joystick.Direction.x > 0 && joystick.Direction.y > 0)
            //{
            //    //rotate right and decelerate
            //    float oldDirections = placedObject.transform.rotation.eulerAngles.y;
            //    oldDirections = oldDirections + joystick.Direction.y * 0.01f;
            //    //placedObject.transform.Rotate(0, oldDirections , 0);
            //    networkText.text = "top right";

            //    placedObject.transform.Rotate(new Vector3(0, Mathf.Atan2(joystick.Direction.y, 0) * 180 / Mathf.PI, 0));
            //}
            //else if (joystick.Direction.x > 0 && joystick.Direction.y < 0)
            //{
            //    //rotate right and accelerate
            //    float oldDirections = placedObject.transform.rotation.eulerAngles.y;
            //    oldDirections = oldDirections + joystick.Direction.y * 0.01f;
            //    //placedObject.transform.Rotate(0, oldDirections, 0);
            //    networkText.text = "bottom right";

            //    placedObject.transform.Rotate(new Vector3(0, Mathf.Atan2(joystick.Direction.y, 0) * 180 / Mathf.PI, 0));
            //}
            //else if (joystick.Direction.x < 0 && joystick.Direction.y < 0)
            //{
            //    //rotate left and accelerate
            //    float oldDirections = placedObject.transform.rotation.eulerAngles.y;
            //    oldDirections = oldDirections - joystick.Direction.y * 0.01f;
            //    //placedObject.transform.Rotate(0, oldDirections, 0);
            //    networkText.text = "bottom left";

            //    placedObject.transform.Rotate(new Vector3(0, Mathf.Atan2(joystick.Direction.y, 0) * 180 / Mathf.PI, 0));
            //}
            //else if (joystick.Direction.x < 0 && joystick.Direction.y > 0)
            //{
            //    //rotate left and decelerate
            //    float oldDirections = placedObject.transform.rotation.eulerAngles.y;
            //    oldDirections = oldDirections - joystick.Direction.y * 0.01f;
            //    //placedObject.transform.Rotate(0, oldDirections, 0);
            //    networkText.text = "top left";

            //    placedObject.transform.Rotate(new Vector3(0, Mathf.Atan2(joystick.Direction.y, 0) * 180 / Mathf.PI, 0));
            //}


            //Moving Object
            if(joystick.Direction.y > 0)
            {
                placedObject.transform.Translate(new Vector3(joystick.Horizontal * 0.01f, 0.0f, joystick.Vertical * 0.01f));
            }
            if (joystick.Direction.y < 0)
            {
                placedObject.transform.Translate(new Vector3(joystick.Horizontal * 0.01f, 0.0f, -joystick.Vertical * 0.01f));
            }


            //placedObject.GetComponent<CarMovement>().StartMoving(hit.Pose.position);

        }
    }

}
