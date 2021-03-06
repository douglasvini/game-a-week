/*****************************************************\
*
*  Copyright (C) 2016, Douglas Knowman 
*  douglasknowman@gmail.com
*
*  Distributed under the terms of GNU GPL v3 license.
*  Always KISS.
*
\*****************************************************/

using UnityEngine;
using System.Collections;
using System;

public struct ScoreBlock
{
    public int score;
    public int linesCleaned;
    public int oneLinesCleaned;
    public int twoLinesCleaned;
    public int threeLinesCleaned;
    public int fourLinesCleaned;
    public int allLinesCleaned;
}
public class GameController : MonoBehaviour
{
    // Public variables.
    public int linescleanedToLevelUp = 10;
    public int accentPoints = 30;
    public int oneLinePoints = 100;
    public int twoLinePoints = 300;
    public int threeLinePoints = 600;
    public int fourLinePoints = 1000;

    public int Level
    {
        get{ return level;}
    }
    // Private variables.
    int level = 1;
    ScoreBlock scoreBlock;
    // Unity functions.
    void Awake()
    {
        EventManager.gameEvent += EventHandler;
    }

    void Start()
    {
        EventManager.levelUpEvent(level);
    }

    void Update ()
    {
       // Debug.Log(String.Format("Points: {0} | 1L={1} | 2L={2} | 3L={3} | 4L={4}",scoreBlock.score,scoreBlock.oneLinesCleaned,scoreBlock.twoLinesCleaned,scoreBlock.threeLinesCleaned,scoreBlock.fourLinesCleaned));
    }
      
    // GameController functions.
    public ScoreBlock GetAllScore()
    {
        return scoreBlock;
    }

    void EventHandler(GameEventType type)
    {
        switch (type)
        {
        case GameEventType.PieceAccent:
            scoreBlock.score += level * accentPoints;
            break;
        case GameEventType.OneLineClean:
            scoreBlock.oneLinesCleaned += 1;
            scoreBlock.score += level *oneLinePoints;
            scoreBlock.allLinesCleaned += 1;
            break;
        case GameEventType.TwoLineClean:
            scoreBlock.twoLinesCleaned += 1;
            scoreBlock.score += level *twoLinePoints;
            scoreBlock.allLinesCleaned += 2;
            break;
        case GameEventType.ThreeLineClean:
            scoreBlock.threeLinesCleaned += 1;
            scoreBlock.score += level * threeLinePoints;
            scoreBlock.allLinesCleaned += 3;
            break;
        case GameEventType.FourLineClean:
            scoreBlock.fourLinesCleaned += 1;
            scoreBlock.score += level * fourLinePoints;
            scoreBlock.allLinesCleaned += 4;
            break;
        }
        if (scoreBlock.allLinesCleaned/level >= linescleanedToLevelUp)
        {
            level+= 1;
            EventManager.levelUpEvent(level);
        }
        
        EventManager.guiUpdate();
    }
}
