using UnityEngine;
using System;
using System.Collections;

public class PlayerCollider : MonoBehaviour
{
    public GameObject jumper;
    public Action onCollideCallback;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.Equals(this.jumper))
        {
            if (this.onCollideCallback != null)
            {
                this.onCollideCallback();
            }
        }
    }
}
