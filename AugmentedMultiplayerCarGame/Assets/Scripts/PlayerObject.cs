using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerObject : NetworkBehaviour {

    public GameObject playerUnitPrefab; 

    private Transform carObjPosition;

    //Joystick
    public Joystick tempJoystick;

    GameObject myPlayerUnit;

    // Use this for initialization
    void Start()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
        
       
        carObjPosition = GameObject.FindWithTag("CarTag").GetComponent<Transform>();
        tempJoystick = GameObject.FindWithTag("Joy").GetComponent<Joystick>();

        CmdspawnMyUnit();
    }


    // Update is called once per frame
    void Update()
    {

        if (isLocalPlayer == false)
        {
            return;
        }

        //myPlayerUnit.transform.Translate(new Vector3(instantiateObjectOnTouch.joystick.Horizontal * 2f, 0.0f, instantiateObjectOnTouch.joystick.Vertical * 2f));
        
    }

    private void FixedUpdate()
    {
        if (isLocalPlayer == false)
        {
            return;
        }

        //myPlayerUnit.transform.Translate(new Vector3(instantiateObjectOnTouch.joystick.Horizontal * 50f, 0.0f, instantiateObjectOnTouch.joystick.Vertical * 50f));

        //myPlayerUnit.transform.Translate(carObjPosition.position);

        //myPlayerUnit.transform.Translate(new Vector3(carObjPosition.position.x , 0.0f , carObjPosition.position.z));

        CmdoveTheCar();

    }


    [Command]
    private void CmdspawnMyUnit()
    {
        GameObject go = Instantiate(playerUnitPrefab);

        //go.transform.Translate(instantiateObjectOnTouch.placedObject.transform.position);

        //go.transform.Translate(carObjPosition.position);
        go.transform.Translate(new Vector3(carObjPosition.position.x, 0.0f, carObjPosition.position.z));
        go.transform.Rotate(-carObjPosition.rotation.eulerAngles.x, -carObjPosition.rotation.eulerAngles.y, -carObjPosition.rotation.eulerAngles.z);

        myPlayerUnit = go;

        NetworkServer.Spawn(go);     

    }

    [Command]
    private void CmdoveTheCar()
    {
        if (myPlayerUnit == null)
        {
            return;
        }

        myPlayerUnit.transform.Translate(new Vector3(tempJoystick.Horizontal * 0.01f, 0.0f, tempJoystick.Vertical * 0.01f));

    }
}
