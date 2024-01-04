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
    


    /*
     * add docs to explain this mechanism (set and remove parent).
     */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            collision.transform.SetParent(this.transform);       
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Player))
        {
            collision.transform.SetParent(null);
        }
    }
}
