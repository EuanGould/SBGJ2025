using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    public float drinkless_speed = 5f;
    public float endrinked_speed = 2.5f;
    public GameObject dialogueUI;

    private float speed = 5f;
    private Rigidbody2D rb;
    private Vector2 input;
    private int drink_stage = 0;
    private SpriteRenderer sprite_renderer;
    private bool engaged = false;
    private GameObject engagedNPC = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite_renderer = GetComponent<SpriteRenderer>();

        ChangeDrinkStage(0);
    }

    void ChangeDrinkStage(int new_value)
    {
        // 0 is no drink
        // 1 is an unpoisoned drink
        // 2 is poisoned drink

        // drink stage shouldnt be able to exceed 2 or go lower than 0
        drink_stage = Mathf.Min(Mathf.Max(new_value, 0), 2);
        
        if (drink_stage == 0)
        {
            sprite_renderer.color = Color.green;
            speed = drinkless_speed;
        }
        else if (drink_stage == 1)
        {
            sprite_renderer.color = Color.yellow;
            speed = endrinked_speed;
        }
        else if (drink_stage == 2)
        {
            sprite_renderer.color = Color.red;
            speed = endrinked_speed;
        }

    }

    public int GetDrinkStage()
    {
        return drink_stage;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.tag + " : " + Time.time);
        
        if(col.gameObject.CompareTag("Bar") && GetDrinkStage() == 0)
        {
            ChangeDrinkStage(1);
        }
        else if (col.gameObject.CompareTag("Poisoner") && GetDrinkStage() == 1)
        {
            ChangeDrinkStage(2);
        }
        else if (col.gameObject.CompareTag("Incident"))
        {
            // we could also put some kind of stun effect here
            ChangeDrinkStage(0);
        }
        else if (col.gameObject.CompareTag("NPC"))
        {
            if (col.gameObject.GetComponent<NPC>().getQuenched() == false && GetDrinkStage() != 0)
            {

                Engage(col.gameObject);
            }
        }
    }

    void Engage(GameObject NPC)
    {
        // begins dialogue with an NPC
        
        dialogueUI.SetActive(true);
        engaged = true;
        NPC.GetComponent<NPC>().Engage();
        engagedNPC = NPC;
        dialogueUI.GetComponent<DialogueUI>().SetupText(NPC.GetComponent<NPC>().optionA, NPC.GetComponent<NPC>().optionB, NPC.GetComponent<NPC>().optionC);
    }

    public void Dialogue(int answer)
    {
        if (answer == engagedNPC.GetComponent<NPC>().correctAnswer)
        {
            // for when a conversation ends in the NPC not drinking your drink
            engagedNPC.GetComponent<NPC>().Quench();
        }
        else
        {
            // for when a conversation ends in the NPC drinking your drink
            if (GetDrinkStage() == 1)
            {
                // the npc drinks your drink and is quenched
                engagedNPC.GetComponent<NPC>().Quench();
                ChangeDrinkStage(0);
            }
            else if (GetDrinkStage() == 2)
            {
                // the npc drinks your poison and dies
                engagedNPC.GetComponent<NPC>().Die();
                ChangeDrinkStage(0);
            }
        }

        Disengage();
    }

    void Disengage()
    {
        print("disengaged");
        dialogueUI.SetActive(false);
        engaged = false;
        engagedNPC.GetComponent<NPC>().Disengage();
    }

    // Update is called once per frame
    void Update()
    {
        if (!engaged)
        {
            input = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();

            input.Normalize();
        }
        else
        {
            input = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = input * speed;
    }

}
