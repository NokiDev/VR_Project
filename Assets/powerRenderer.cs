using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerRenderer : MonoBehaviour {

    float maxEnergy = 100;
    Image fillImg;
    float energy;
	// Use this for initialization
	void Start () {
        fillImg = this.GetComponent<Image>();
        energy = this.GetComponentInParent<HandsData>().CurrentEnergy;
    }
	
	// Update is called once per frame
	void Update () {
		if(energy > 0)
        {
            fillImg.fillAmount = energy / maxEnergy;
        }
	}
}
