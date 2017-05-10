using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    private bool isGameEnding = false;
    private float gameEndTime;

    // Player vars for overriding animation
    public Player player;
    private float localGravity = 2f;

    // Jumper vars for overriding animation
    public Jumper jumper;

    // Shadow vars
    public Shadow shadow;

    float gameEndDuration = 2;

    public void EndGame()
    {
        isGameEnding = true;
        gameEndTime = Time.time;

        if (Debug.isDebugBuild)
            Debug.Log("The game has ended");

        // tell the player to ignore inputs
        this.player.Dead();

        // tell the jumper to stop
        this.jumper.Stop();

        // destory the Shadow
        Destroy(shadow.gameObject);

        // TODO play losing sound
    }

    void Start()
    {
        GameObject.Find("ScoreLoader").GetComponent<ScoreLoader>().ResetScore();
    }

    void Update()
    {
        if (isGameEnding)
        {
            // have the player "fall" like in sonic
            this.localGravity -= 9.81f * Time.deltaTime;
            this.player.transform.position = new Vector3(
                this.player.transform.position.x,
                this.player.transform.position.y + this.localGravity * Time.deltaTime,
                this.player.transform.position.z
            );


            // Go to the ScoreScene after some cooldown
            if (Time.time > this.gameEndDuration + this.gameEndTime)
            {
                SceneManager.LoadScene("ScoreScene");
            }
        }
    }
}
