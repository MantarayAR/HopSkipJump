using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Score : MonoBehaviour
{
    public int highScore = 0;
    public int score = 0;

    public Text highScoreComponent;
    public Text scoreComponent;

    void OnGUI()
    {
        // draw high score
        this.highScoreComponent.text = "Best " + this.highScore;

        // draw score
        this.scoreComponent.text = "" + this.score;
    }
}
