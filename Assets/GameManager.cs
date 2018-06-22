using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : UnDestroyable {

    public enum GameEndType
    {
        WIN,
        LOOSE,
        ABANDON
    }

    public const float gameDuration = 60.0f;//1 min 
    uint targetDestroyedCount = 0;
    float currentTime = 0.0f;
    public bool started = false;

    public delegate void GameEndTypeEmitter();

    public event HandsData.EmptyEmitter OnGameStart;

    public event HandsData.UIntegerEmitter OnGameEnd;
    public event HandsData.UIntegerEmitter OnScoreChanged;
    public event HandsData.FloatEmitter timeLeft;

	// Use this for initialization
	void Start () {
        NewGame();
	}
	
	// Update is called once per frame
	void Update () {
        if(started)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= gameDuration)
            {
                GameEnd();
            }
            else
            {
                if (timeLeft != null) timeLeft(gameDuration - currentTime);

            }

        }
    }

    void NewGame()
    {
        if(!started)
        {
            targetDestroyedCount = 0;
            currentTime = 0.0f;
            started = true;
            if (OnGameStart != null)
                OnGameStart();
        }
    }

    void GameEnd()
    {
        started = false;
        if (OnGameEnd != null)
            OnGameEnd(targetDestroyedCount);
        //LoadScene.
        //CheckScore.
    }

    void IncrementScore(uint amount)
    {
        targetDestroyedCount += amount;
        OnScoreChanged(targetDestroyedCount);
    }
}
