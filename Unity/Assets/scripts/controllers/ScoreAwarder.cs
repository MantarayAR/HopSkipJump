using UnityEngine;
using UnityEngine.Audio;

public class ScoreAwarder : MonoBehaviour
{
    private bool shouldAward = true;
    private ScoreLoader score;
    private Jumper jumper;
    private AudioSource sound;

    void Start()
    {
        score = GameObject.Find("ScoreLoader").GetComponent<ScoreLoader>();
        jumper = GameObject.Find("Jumper").GetComponent<Jumper>();
        sound = GameObject.Find("Points Audio Source").GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (this.shouldAward)
        {
            score.Add();
            jumper.IncreaseDifficulty();
            sound.Play();
            this.shouldAward = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        this.shouldAward = true;
    }
}
