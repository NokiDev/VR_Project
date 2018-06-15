using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class HandsInputs : MonoBehaviour {

    public KeyCode debugKey = KeyCode.Q; // Pressed - Pull objects, Release - Push objects.

    public Pusher PusherGO;
    public Puller PullerGO;

	// Use this for initialization
	void Start () {
        Debug.Assert(PusherGO != null && PullerGO != null);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger) || Input.GetKey(debugKey))
        {//Pull objects.
            PullerGO.Pull();//Run Animation.
        }

        if (OVRInput.GetUp(OVRInput.Touch.PrimaryIndexTrigger) || Input.GetKeyUp(debugKey))
        {
            PullerGO.Stop();

            PusherGO.Push();
        }
    }
}
