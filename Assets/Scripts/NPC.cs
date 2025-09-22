using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class NPC : MonoBehaviour
{
    public GameObject goals_object;
    public float speed;

    private Transform[] transforms;
    private int number_of_goals;
    private int goal_num = 0;
    private Vector2 goal;
    private Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        number_of_goals = goals_object.GetComponentsInChildren<Transform>().Length;
        transforms = goals_object.GetComponentsInChildren<Transform>();

        rb = GetComponent<Rigidbody2D>();

        goal = transforms[goal_num].position;
    }

    void FixedUpdate()
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
    }
}
