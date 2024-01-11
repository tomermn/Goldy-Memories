using System.Collections;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// Manages a rain event triggered when the player reaches a specific checkpoint.
/// </summary>
public class RainEvent : MonoBehaviour
{
    [SerializeField]
    private Image screenOverlay;

    [SerializeField]
    private ParticleSystem rainParticleSystem;

    [SerializeField]
    private GameObject lightning;

    private float flashDuration = 1f;
    private float fadeDuration = 1.5f;
    private bool hasTriggeredRain = false;

    private void Start()
    {
        rainParticleSystem.Stop();
    }

    /// <summary>
    /// Triggers a storm event when the player reaches a specific checkpoint.
    /// The sequence of events includes darkening the screen, simulating lightning, and invoking rain.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggeredRain && collision.CompareTag(Tags.Player)) // Adjust the tag as needed
        {
            StartCoroutine(DarkenScreen());
            StartCoroutine(FlashScreen());
            StartRaining(1f);

            hasTriggeredRain = true;
        }
    }

    private void StartRaining(float delay)
    {
        Invoke(nameof(StartRaining), delay);
    }

    private void StartRaining()
    {
        rainParticleSystem.Play();
    }

    /// <summary>
    /// Coroutine for simulating a lightning flash with adjustable opacity.
    /// </summary>
    private IEnumerator FlashScreen()
    {
        lightning.SetActive(true);

        // Parameters for adjusting the lightning opacity
        float startAlpha = 1f;
        float endAlpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            SetLightningAlpha(alpha, lightning);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(flashDuration);
        lightning.SetActive(false);
    }



    private void SetLightningAlpha(float alpha, GameObject lightning)
    {
        SpriteRenderer spriteRenderer = lightning.GetComponent<SpriteRenderer>();
        Color spriteColor = spriteRenderer.color;
        spriteColor.a = alpha;
        spriteRenderer.color = spriteColor;
    }

    /// <summary>
    /// Coroutine for darkening the screen during the storm.
    /// </summary>
    private IEnumerator DarkenScreen()
    {
        float duration = 1f; // Adjust the duration as needed
        float targetAlpha = 0.666f; // Adjust the darkness level

        float startTime = Time.time;
        while (Time.time < startTime + duration)
        {
            float lerpValue = (Time.time - startTime) / duration;
            screenOverlay.color = new Color(0, 0, 0, Mathf.Lerp(0, targetAlpha, lerpValue));
            yield return null;
        }
    }








}