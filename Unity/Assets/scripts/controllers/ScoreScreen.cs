using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class ScoreScreen : Score
{
    public AdvertisementHelper advertisements;

    void Awake()
    {
        // 50-50 chance of playing an ad
		if (UnityEngine.Random.value < .5f)
            advertisements.ShowAd();
        GameObject.Find("ScoreLoader").GetComponent<ScoreLoader>().Save();
    }

    void OnGUI()
    {
        // draw high score
        this.highScoreComponent.text = "Best " + this.highScore;

        // draw score
        this.scoreComponent.text = "Score " + this.score;
    }
}
