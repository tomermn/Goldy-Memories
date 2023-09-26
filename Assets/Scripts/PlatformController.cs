using UnityEngine;

public class PlatformController : MonoBehaviour
{

    public Transform posA, posB;
    public int platformSpeed;
    Vector2 targetPos;
    // Start is called before the first frame update
    void Start()
    {
        targetPos = posB.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f) targetPos = posB.position;
        if (Vector2.Distance(transform.position,posB.position) < .1f) targetPos = posA.position;

        transform.position = Vector2.MoveTowards(transform.position, targetPos, platformSpeed * Time.deltaTime);
        
    }
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("on moving");
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
            Debug.Log("on moving");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("exit moving");
            collision.transform.SetParent(null);
        }
    }
}
