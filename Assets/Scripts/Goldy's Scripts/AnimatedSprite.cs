
using UnityEngine;

/// <summary>
/// Represents an animated sprite component that cycles through an array of sprites at a specified frame rate.
/// </summary>
public class AnimatedSprite : MonoBehaviour
{
    [SerializeField]
    private Sprite[] sprites;        // Array of sprites for animation.
    [SerializeField]
    protected float framerate;
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

    /// <summary>
    /// Advances the animation frame, looping back to the first frame when the end is reached.
    /// </summary>
    protected void Animate()
    {
        frame++;

        if (frame >= sprites.Length)
        {
            frame = 0;
        }

        spriteRenderer.sprite = sprites[frame];
    }

    public Sprite getCurrentSprite()
    {
        return sprites[frame];
    }
}