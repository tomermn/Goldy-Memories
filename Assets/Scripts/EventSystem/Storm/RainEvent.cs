using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.Port;

public class RainEvent : MonoBehaviour
{
    public Image screenOverlay;
    public ParticleSystem rainParticleSystem;
    public GameObject lightning;
    public float flashDuration = 1f;
    public float fadeDuration = 1.5f;

    private bool hasTriggeredRain = false;

    private void Start()
    {
        rainParticleSystem.Stop();
    }

    /*
     * This function triggers a storm event when the player reaches a specific storm checkpoint.
    The sequence of events is as follows:
    1. The screen will darken, simulating clouds gathering.
    2. A flash, resembling lightning, will occur.
    3. The particle system will be invoked, leading to a raining event.

     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggeredRain && collision.CompareTag("Player")) // Adjust the tag as needed
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
    private IEnumerator FlashScreen()
    {
        lightning.SetActive(true);

        //Parameters for adjusting the lightning opacity
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