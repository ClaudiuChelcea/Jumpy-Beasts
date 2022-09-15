using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Controller : MonoBehaviour
{
    // Variables
    protected Rigidbody2D player;
    public float velocity = 2.5f;
    [SerializeField] protected GameObject side_character;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player jump
        try
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                // Player jumps
                var remember_position = this.transform.position;
                player.velocity = Vector2.up * velocity;

                // Spawn SC under him
                GameObject SC = Instantiate(side_character);
                SC.transform.position = remember_position;
            }
        }
        catch(Exception any_exception)
        {
            Debug.Log(any_exception.Message);
        } 
    }

    // Check collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "MarginWall")
        {
            // Add points to score and destroy characters that touch that margin
            Destroy(this.gameObject);

            // TO DO: To add death
        }
    }
}
