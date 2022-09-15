using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Controller : MonoBehaviour
{
    // Variables

    // Player
    protected Rigidbody2D player;
    public float velocity = 2.5f;
    [SerializeField] protected GameObject side_character;
    public Vector3 explosion_scale = new Vector3(0.2f,0.2f,0.2f); // Cube size
    public float particle_mass = 0.02f, particle_drag = 8; // Cube weight
    public Vector3 explosion_particles_spread_3D = new Vector3(5, 5, 5); // The explosion with be a 3 dimensional matrix, rows columns and width
    float cubesPivotDistance;
    Vector3 cubesPivot;
    public float explosionForce = 50f;
    public float explosionRadius = 4f;
    public float explosionUpward = 0.4f;
    protected bool IamALIVE = true;
    public static bool MCamALIVE = true;
    public float destroy_piece_time = 3f;
    public GameObject pieces_parent, SC_parent;
    public float speedTowardsMiddleSkateboard = 10f;
    public float middle_of_skateboard = -1.33f;
    [SerializeField] protected bool moving_back = false;
    public float player_offset = 10f;
    public float destroy_SC_time = 10f;

    // Start is called before the first frame update
    void Start()
    {
        IamALIVE = true;
        player = GetComponent<Rigidbody2D>();
        cubesPivotDistance = (explosion_scale.x * explosion_particles_spread_3D.x) / 2;
        cubesPivot = new Vector3(cubesPivotDistance, cubesPivotDistance, cubesPivotDistance); // how far the explosion will launch
        moving_back = false;    
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
                GameObject SC = Instantiate(side_character,player.transform.position, Quaternion.Euler(player.transform.eulerAngles));

                // Set parent
                SC.transform.parent = SC_parent.transform;

                // Destroy clone after a time
                Destroy(SC, destroy_SC_time);
            }
        }
        catch(Exception any_exception)
        {
            Debug.Log(any_exception.Message);
        }

        // If we are not colliding with anything, bring character to the middle of the skateboard
        if(moving_back == false)
        {
            this.transform.position = new Vector2(middle_of_skateboard, this.transform.position.y);
        }

        // Send alive status for MC
        if(this.gameObject.tag == "MainCharacter")
        {
            MCamALIVE = IamALIVE;
        }
    }

    // Check collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "MarginWall" && this.gameObject.tag == "MainCharacter")
        { // Main character
            IamALIVE = false;

            // Add points to score and destroy characters that touch that margin
            var keep_position = this.gameObject.transform.position;
            Destroy(this.gameObject);

            // Death
            DieMC(keep_position);

        } else if(collision.collider.tag == "MarginWall")
        { // Side character
            IamALIVE = false;
            Destroy(this.gameObject);
            ++GameManager.score;
        }
        else if(collision.collider.tag == "Column")
        {
            moving_back = true;
        }
    }

    // Death of character
    private void DieMC(Vector3 keep_position)
    {
        IamALIVE = false;
        for (int i = 0; i < explosion_particles_spread_3D.x; ++i)
        {
            for (int j = 0; j < explosion_particles_spread_3D.y; ++j)
            {
                for (int k = 0; k < explosion_particles_spread_3D.z; ++k)
                {
                    explodeMyself(keep_position);
                }
            }
        }

        Vector3 explosionPos = keep_position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(explosionForce, keep_position, explosionRadius, explosionUpward);
            }
        }
    }

    // Explode
    private void explodeMyself(Vector3 position)
    {
        GameObject piece;
        piece = GameObject.CreatePrimitive(PrimitiveType.Cube);

        // Position and scale
        piece.transform.position = position + new Vector3(explosion_scale.x * explosion_particles_spread_3D.x,
                                                          explosion_scale.y * explosion_particles_spread_3D.y,
                                                          explosion_scale.z * explosion_particles_spread_3D.z) - cubesPivot;
        piece.transform.localScale = explosion_scale; // Cube size

        piece.AddComponent<Rigidbody>();
        piece.GetComponent<Rigidbody>().mass = particle_mass; // Mass of particle
        piece.GetComponent<Rigidbody>().drag = particle_drag; // Drag of particle
        piece.AddComponent<Renderer>();
        piece.GetComponent<Renderer>().material.color = new Color(0, 0, 255); // blue
        piece.transform.parent = pieces_parent.transform;

        Destroy(piece, destroy_piece_time);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Column")
        {
            moving_back = false;
        }
    }
}
