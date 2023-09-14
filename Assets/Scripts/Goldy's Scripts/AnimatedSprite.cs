
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate =(1/6f);

    protected SpriteRenderer spriteRenderer;
    protected int frame;

    virtual protected void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
    }

    protected void OnDisable()
    {
        CancelInvoke();
    }

    protected void Animate()
    {
        frame++;
        Debug.Log("frame number: " + frame);

        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        spriteRenderer.sprite = sprites[frame];
    }
}