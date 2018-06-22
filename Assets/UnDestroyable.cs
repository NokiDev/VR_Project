using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnDestroyable : MonoBehaviour {

    // Use this for initialization
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this);
    }

    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
