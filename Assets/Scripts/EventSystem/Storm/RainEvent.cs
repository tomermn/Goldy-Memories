using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
        //SpawnBoundaryRandomally();
    }

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

        float startAlpha = 1f;
        float endAlpha = 0f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            setLightningAlpha(alpha, lightning);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(flashDuration);
        lightning.SetActive(false);
    }



    private void setLightningAlpha(float alpha, GameObject lightning)
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




    /**
     * this method randomally spawn the event at given points
     */

    //private void SpawnBoundaryRandomally()
    //{
    //    GameObject[] boundaryPoints = GameObject.FindGameObjectsWithTag("BoundaryPoint");
    //    Debug.Log(boundaryPoints.Length);
        // Choose a random available spawn point index.
    //    int randomIndex = UnityEngine.Random.Range(0, boundaryPoints.Length);
    //    Transform chosenSpawnPoint = boundaryPoints[randomIndex].transform;

        // Set the position of the collectible to the chosen spawn point's position.
    //    transform.position = chosenSpawnPoint.position;

    //}




}