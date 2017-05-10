using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Player player;
    private float startY;
    private float maxDiff;
    private float originalScale;

    void Start()
    {
        this.startY = player.transform.localPosition.y;
        // TODO hardcoded for now, see the floor variable in Player.cs
        this.maxDiff = 1.3f - player.transform.localPosition.y;
        this.originalScale = this.transform.localScale.x;
    }

    void Update()
    {
        float diff = player.transform.localPosition.y - this.startY;

        // Scale the shadow by the height
        float scale = (-0.5f * diff) / this.maxDiff + 1;

        this.transform.localScale = new Vector3(
            this.originalScale * scale,
            this.originalScale * scale,
            this.transform.localScale.z
        );
    }
}
