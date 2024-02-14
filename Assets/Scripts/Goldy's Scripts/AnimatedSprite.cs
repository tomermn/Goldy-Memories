
using UnityEngine;

/// <summary>
/// Represents an animated sprite component that cycles through an array of sprites at a specified frame rate.
/// </summary>
public class AnimatedSprite : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;        // Array of sprites for animation.
    [SerializeField] protected float framerate;
    protected SpriteRenderer SpriteRenderer;
    protected int Frame;

    public Sprite GetCurrentSprite()
    {
        return sprites[Frame];
    }

    protected virtual void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
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
        Frame++;

        if (Frame >= sprites.Length)
        {
            Frame = 0;
        }

        SpriteRenderer.sprite = sprites[Frame];
    }

    
}