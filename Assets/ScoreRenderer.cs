using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRenderer : MonoBehaviour {

    private GameManager gameManager;
    private Text text;
    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.OnScoreChanged += updateScore;
        text = GetComponent<Text>();

    }

    // Update is called once per frame
    void Update () {
		
	}
    public void updateScore(uint score)
    {
        text.text = "Score : " + score;
    }
}
