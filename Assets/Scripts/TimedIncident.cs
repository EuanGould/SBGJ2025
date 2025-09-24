using UnityEngine;

public class TimedIncident : MonoBehaviour
{
    public float time_until_active;
    private float time_left;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        time_left = time_until_active;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (time_left > 0)
        {
            time_left -= Time.deltaTime;

            if (time_left < 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
    }
}
