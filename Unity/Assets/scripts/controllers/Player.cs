using UnityEngine;
using UnityEngine.Audio;

enum PlayerState {
    Normal,
    Jump,
    PreJump,
    Dead
};

public class Player : MonoBehaviour
{
    private PlayerState playerState = PlayerState.Normal;
    private float acceleration = 0.0f;
    private float velocity;
    private Vector3 originalPosition = Vector3.zero;
    private float factor = 3;
    private float decayFactor = 1f;
    private float gravity = 9.8f;
    private float floor = 1.3f;
    public AudioSource soundJump;
    public AudioSource soundHit;
    public GameObject yiss;
    public GameObject merr;
    public GameObject nooo;

    void Start()
    {
        PhoneInput phoneInput = this.GetComponent<PhoneInput>();
        phoneInput.onJumpCallback = this.onJumpCallback;
        phoneInput.onCrouchCallback = this.onCrouchCallback;

        PlayerCollider playerCollider = this.GetComponent<PlayerCollider>();
        playerCollider.onCollideCallback = this.onCollideCallback;

        this.originalPosition = this.transform.position;
    }

    void Update()
    {
        switch (this.playerState)
        {
            case PlayerState.Jump:
                this.yiss.GetComponent<Renderer>().enabled = true;
                this.merr.GetComponent<Renderer>().enabled = false;
                this.nooo.GetComponent<Renderer>().enabled = false;
                break;
            case PlayerState.Dead:
                this.yiss.GetComponent<Renderer>().enabled = false;
                this.merr.GetComponent<Renderer>().enabled = false;
                this.nooo.GetComponent<Renderer>().enabled = true;
                break;
            default:
                this.yiss.GetComponent<Renderer>().enabled = false;
                this.merr.GetComponent<Renderer>().enabled = true;
                this.nooo.GetComponent<Renderer>().enabled = false;
                break;
        }

        if (this.playerState == PlayerState.Dead)
        {
            return;
        }

        if (Debug.isDebugBuild)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Debug click jump");

                this.onJumpCallback(2.0f);
            }
        }

        if (this.playerState == PlayerState.Jump)
        {
            // Apply gravity to velocity
            this.acceleration -= decayFactor * Time.deltaTime * gravity;
            this.velocity += factor * Time.deltaTime * this.acceleration;
            float newPosition = this.transform.position.y + Time.deltaTime * this.velocity;

            // Lock the position between two points;
            // 0 is the center. Hardcoded for now :(
            if (newPosition > this.floor)
            {
                newPosition = this.floor;
                this.acceleration = 0f;
                this.velocity = 0f;
            }
            else if (newPosition < this.originalPosition.y)
            {
                newPosition = this.originalPosition.y;
                this.acceleration = 0f;
                this.velocity = 0f;
            }

            this.transform.position = new Vector3(this.transform.position.x, newPosition, this.transform.position.z);

            // Did we settle on the floor?
            if (this.originalPosition == this.transform.position)
            {
                this.playerState = PlayerState.Normal;
            }
        }
    }

    public void onJumpCallback(float acceleration)
    {
        if (this.playerState != PlayerState.Jump && this.playerState != PlayerState.Dead)
        {
            this.playerState = PlayerState.Jump;
            this.acceleration = factor * acceleration;
            this.velocity = 3 * factor * acceleration;
            this.soundJump.Play();
        }
    }

    public void onCrouchCallback(float acceleration)
    {
        if (this.playerState == PlayerState.Normal)
        {
            this.playerState = PlayerState.PreJump;
        }
    }

    public void onCollideCallback()
    {
        Game game = GameObject.Find("Game").GetComponent<Game>();

        // NOTE This is where we would implement health

        this.soundHit.Play();
        game.EndGame();
    }

    public void Dead()
    {
        this.playerState = PlayerState.Dead;
    }
}
