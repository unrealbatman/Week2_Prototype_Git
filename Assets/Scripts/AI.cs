using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    private GameObject player;
    private float distanceToPlayer;
    private Vector2 directionToPlayer;
    private Rigidbody2D rb;
    private bool isGrounded = false;


    public float moveSpeed = 3f;
     float jumpForce = 30f;
    public LayerMask groundLayer;
    public Transform groundCheck;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
       /*distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        Debug.Log(distanceToPlayer);
        directionToPlayer =   player.transform.position - this.transform.position;
        if(player.transform.position.y != this.transform.position.y)
        {
            //rb.AddForce(directionToPlayer );
            this.transform.Translate(directionToPlayer) ;
        }*/


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded)
        {
            // Move towards player
            Transform playerPos = player.transform;
            if (playerPos.position.x > transform.position.x)
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            else
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

            float distanceToPlayer = Mathf.Abs(playerPos.position.x - transform.position.x);

            // Calculate required jump force based on distance to player
            //float jumpForce = CalculateJumpForce(distanceToPlayer);

            if (playerPos.position.y > transform.position.y + 1.0f)
                rb.AddForce(new Vector2(0, jumpForce));
            else
                rb.AddForce(new Vector2(0, -jumpForce));
        }

    }



}










