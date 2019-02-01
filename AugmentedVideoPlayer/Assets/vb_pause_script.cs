using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Vuforia;

public class vb_pause_script : MonoBehaviour , IVirtualButtonEventHandler {

    public GameObject vb_pause_obj, vb_speed_obj;

    public List<VideoClip> videoList;

    private GameObject quad;
    private VideoPlayer videoPlayer;

    bool showRonaldo = false;
    

	// Use this for initialization
	void Start () {

        vb_pause_obj = GameObject.Find("PauseButton");
        vb_pause_obj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        vb_speed_obj = GameObject.Find("SpeedButton");
        vb_speed_obj.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

        quad = GameObject.FindGameObjectWithTag("VideoQuad");
        videoPlayer = quad.GetComponent<VideoPlayer>();
    }

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if(vb.name == "PauseButton")
        { 
            Debug.Log("Pressed pause");
            if(videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
        }
        if (vb.name == "SpeedButton")
        {


            //if (videoPlayer.isPlaying)
            //{
            //    videoPlayer.playbackSpeed = videoPlayer.playbackSpeed / 5.0F;
            //}


            if (!showRonaldo)
            {
                showRonaldo = true;
                Debug.Log("Pressed speed");
                if (videoPlayer.isPlaying)
                {
                    videoPlayer.clip = videoList[1];
                }
            }
            else if (showRonaldo)
            {
                showRonaldo = false;
                Debug.Log("Pressed speed");
                if (videoPlayer.isPlaying)
                {
                    videoPlayer.clip = videoList[0];
                }
            }
        }

    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
        if (vb.name == "PauseButton")
        {
            Debug.Log("Released");
            if (!videoPlayer.isPlaying)
            {
                videoPlayer.Play();
            }
        }
        //if (vb.name == "SpeedButton")
        //{

        //    if (videoPlayer.isPlaying)
        //    {
        //        videoPlayer.playbackSpeed = videoPlayer.playbackSpeed * 5.0F;
        //    }
        //}
    }



    // Update is called once per frame
    void Update () {
		
	}


}
