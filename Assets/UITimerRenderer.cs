using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimerRenderer : MonoBehaviour {

    GameManager manager;
    Text text;
    public string unit = "s";
    Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        text = GetComponent<Text>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.OnGameStart += StartDisplay;
        manager.OnGameEnd += StopDisplay;
        manager.timeLeft += UpdateTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void StartDisplay()
    {
        animator.SetBool("started", true);
        gameObject.SetActive(true);
    }

    void StopDisplay(uint score)
    {
        animator.SetBool("started", false);
        gameObject.SetActive(false);
    }

    void UpdateTime(float timeLeft)
    {
        text.text = ((int)(timeLeft * 10) / 10) + " " + unit;
        animator.SetFloat("timeLeft", timeLeft);
    }
}
