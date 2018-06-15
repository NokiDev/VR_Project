using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puller : MonoBehaviour {

    public Vector3 originForce; //Force on origin. Decrease over distance.
    public Vector3 maxPullDistance; // Max distance in which the objects can be pulled.


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		/// Animation / Coroutines
	}

    public void Pull()
    {
        //Pull effect code.
    }

    public void Stop()
    {
        //Stop pull effect nicely.
        //Stop animation.
    }
}
