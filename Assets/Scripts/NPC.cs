using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class NPC : MonoBehaviour
{
    public GameObject goals_object;
    public float base_speed;
    public float queched_speed;
    public float quenchability;
    public int correctAnswer;
    public string optionA;
    public string optionB;
    public string optionC;
    public string dialogueText;

    private float speed;
    private bool alive = true;
    private Transform[] transforms;
    private int number_of_goals;
    private int goal_num = 0;
    private Vector2 goal;
    private Rigidbody2D rb;
    private float quench_timer;
    private bool engaged = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        number_of_goals = goals_object.GetComponentsInChildren<Transform>().Length;
        transforms = goals_object.GetComponentsInChildren<Transform>();

        rb = GetComponent<Rigidbody2D>();

        goal = transforms[goal_num].position;

        speed = base_speed;

        // replace with sprite change
        gameObject.GetComponent<SpriteRenderer>().color = Color.pink;
        // replace with sprite change
    }

    void FixedUpdate()
    {
        if (alive && !engaged)
        {
            Vector2 diff_to_goal = goal - new Vector2(transform.position.x, transform.position.y);

            rb.linearVelocity = diff_to_goal.normalized * speed;

            if (diff_to_goal.magnitude < 0.01 * speed)
            {
                gameObject.transform.position = goal;

                goal_num += 1;
                goal_num %= number_of_goals;
                goal = transforms[goal_num].position;
            }

            if (quench_timer > 0)
            {
                quench_timer -= Time.deltaTime;

                if (quench_timer <= 0)
                {
                    unQuench();
                }
            }
        }
    }


    public void Engage()
    {
        engaged = true;
        rb.linearVelocity = Vector2.zero;
    }

    public void Disengage()
    {
        engaged = false;
    }

    public bool getQuenched()
    {
        return quench_timer > 0;
    }

    public void Quench()
    {
        quench_timer = quenchability;
        gameObject.GetComponent<SpriteRenderer>().color = Color.deepPink;
        speed = queched_speed;
    }

    void unQuench()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.pink;
        speed = base_speed;
    }

    public void Die()
    {
        alive = false;
        gameObject.tag = "Incident";
        rb.linearVelocity = Vector2.zero;

        // replace with sprite change
        gameObject.GetComponent<SpriteRenderer>().color = Color.darkMagenta;
        // replace with sprite change
    }
}
