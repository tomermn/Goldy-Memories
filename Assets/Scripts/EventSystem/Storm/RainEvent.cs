using System.Collections;
using UnityEngine;
using UnityEngine.UI;



/// <summary>
/// Manages a rain event triggered when the player reaches a specific checkpoint.
/// </summary>
public class RainEvent : MonoBehaviour
{

    [SerializeField] private Image screenOverlay;
    [SerializeField] private ParticleSystem rainParticleSystem;
    [SerializeField] private GameObject lightning;

    private const float FlashDuration = 1f;
    private const float FadeDuration = 1.5f;
    private const float FlashStartAlpha = 0f;
    private const float FlashEndAlpha = 1f;
    private const float DarkScreenDuration = 1f;
    private const float DarknessScreenTarget = 2 / 3f;
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
        if (!hasTriggeredRain && collision.CompareTag(PlayerMovement.PlayerTag)) // Adjust the tag as needed
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
        var elapsedTime = 0f;

        while (elapsedTime < FadeDuration)
        {
            float alpha = Mathf.Lerp(FlashStartAlpha, FlashEndAlpha, elapsedTime / FadeDuration);
            SetLightningAlpha(alpha, lightning);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(FlashDuration);
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
        var startTime = Time.time;
        while (Time.time < startTime + DarkScreenDuration)
        {
            var lerpValue = (Time.time - startTime) / DarkScreenDuration;
            screenOverlay.color = new Color(0, 0, 0, Mathf.Lerp(0, DarknessScreenTarget, lerpValue));
            yield return null;
        }
    }








}