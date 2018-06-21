using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class powerRenderer : MonoBehaviour {

    float maxEnergy;
    Image fillImg;
	// Use this for initialization
	void Start () {
        fillImg = this.GetComponent<Image>();
        maxEnergy = this.GetComponentInParent<HandsData>().maxEnergy;
        this.GetComponentInParent<HandsData>().energyChanged += updateEnergy;
    }
	
	// Update is called once per frame
	void Update () {

	}
    void updateEnergy(float currentEnergy)
    {
        fillImg.fillAmount = currentEnergy / maxEnergy;
    }
}
