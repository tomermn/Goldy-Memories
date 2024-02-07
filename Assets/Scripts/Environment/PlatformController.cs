using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private Transform posA, posB;

    [SerializeField] private int platformSpeed;

    [SerializeField] private Vector2 targetPos;
    
    private void Start()
    {
        targetPos = posB.position;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f)
        {
            targetPos = posB.position;
        }
        if (Vector2.Distance(transform.position, posB.position) < .1f)
        {
            targetPos = posA.position;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetPos, platformSpeed * Time.deltaTime);
    }
    


    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.PLAYER_TAG))
        {
            collision.transform.SetParent(this.transform);       
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.PLAYER_TAG))
        {
            collision.transform.SetParent(null);
        }
    }
}
