using UnityEngine;
using System;

public class Jumper : MonoBehaviour
{
    private Vector3 center = Vector3.zero;
    private Vector2 oval = new Vector2(150, 300);
    private bool stop = false;
    public SpriteRenderer sprite;
    private float speed = 1;
    private float alpha = Mathf.PI;

    void Update()
    {
        if (this.stop)
            return;

        float circle = (2 * Mathf.PI);
        this.alpha += Time.deltaTime * -this.speed * 2 % circle;

        float x = this.center.x + (this.oval.x * Mathf.Cos(this.alpha));
        float y = this.center.y + (this.oval.y * Mathf.Sin(this.alpha));

        Vector3 position = new Vector3(x, y, this.transform.localPosition.z);

        this.transform.localPosition = position;
        sprite.transform.rotation = Quaternion.AngleAxis((this.alpha + 3 * Mathf.PI / 2 ) * 180 / Mathf.PI, Vector3.forward);
    }

    public void Stop()
    {
        this.stop = true;
    }

    public void IncreaseDifficulty()
    {
        this.speed *= 1.05f;

        SpriteRenderer sprite = GameObject.Find("jumper").GetComponent<SpriteRenderer>();
        Color color = new Color(1, 1, 1, 1);

        switch ((int)(this.speed * 1.5))
        {
            case 0:
            case 1:
                color = new Color(1, 1, 1, 1);
                break;
            case 2:
                color = new Color(0, 1, 1, 1);
                break;
            case 3:
                color = new Color(1, 0, 1, 1);
                break;
            case 4:
                color = new Color(1, 1, 0, 1);
                break;
            case 5:
                color = new Color(1, 1, 0, 1);
                break;
            default:
                color = new Color(0, 0, 0, 1);
                break;
        }

        sprite.color = color;

        if (Debug.isDebugBuild)
            Debug.Log("Increasing Difficulty to " + this.speed);
    }
}
