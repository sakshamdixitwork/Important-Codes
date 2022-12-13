using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// The Data model for Game
/// </summary>

[Serializable]
public class GameData
{

    public int coinCount = 1;       // tracks the no. of coins collected
  //  public int score;           // for tracking the score
    public int lives = 3;           // tracks the lives
    public LevelData[] levelData; // for tracking level data like level unlocked, stars awarded , level no.

    public bool isFirstBoot;    // for initializing data when game started for the first time

    public bool playSound;
    public bool playMusic;

}
