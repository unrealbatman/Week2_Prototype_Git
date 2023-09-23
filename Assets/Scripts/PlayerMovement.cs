using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float axis;
    private Light2D flashLight;

    [SerializeField]
    private float speed =1f;

    [SerializeField]
    private float rotationSpeed;

    private bool canToggleLight = true;
    private bool isGrounded = false;
    private float flashlightTimer = 0f;
    private Animator animator;

    public int jumpForce = 7;
    public float lightDuration = 5f;



    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        flashLight = this.GetComponentInChildren<Light2D>();
        animator = GetComponent<Animator>();
    }

    // Go back to Title Screen if player dies (hits kill zone)
    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag("Gate")){
            SceneManager.LoadScene("Title"); // Create and change to Win scene
        }
    }
    // Update is called once per frame
    void Update()
    {
        axis = Input.GetAxisRaw("Horizontal");


        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            float rotDir = axis == 1 ? 0 : 180;
            rb.velocity = new Vector2(axis * speed ,rb.velocity.y);
            this.transform.localRotation = Quaternion.Euler(0,rotDir * rotationSpeed,0);
        }
        if((axis>0 || axis<0) && isGrounded==true)
        {
            animator.SetInteger("speed", Mathf.Abs((int)rb.velocity.x));


        }
        else
        {
            animator.SetInteger("speed", 0);


        }

        // Use this if you want player to be like kirby/flappy bird with the ability to float around

        if (Input.GetButtonDown("Jump") )
        {
            rb.AddForce(new Vector2(0, jumpForce));
            animator.SetTrigger("Jump");

        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false; // Player has jumped, so they're not grounded anymore
            canToggleLight = true; // Player has jumped, so they can now toggle the light
            animator.SetTrigger("Jump");

        }


        if (flashlightTimer > 0)
        {
            flashlightTimer -= Time.deltaTime; // Decrement the timer
        }
        else
        {
            flashLight.enabled = false; // Turn off the flashlight when timer reaches 0
        }

        // Use this if you want player to only be able to jump if they are grounded (on a platform)
        /*
        grounded = Physics2D.OverlapCircle(feet.position, .3f, whatIsGround);
        if(Input.GetButtonDown("Jump") && grounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
        }
        */
    }






    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            isGrounded = true; // Player has landed on the ground
            if (canToggleLight)
            {
                flashLight.enabled = true; // Activate the flashlight
                flashlightTimer = lightDuration; // Reset the timer
                canToggleLight = false; // Player cannot toggle light until they jump again
            }
        }
        if (collision.transform.CompareTag("Kill"))
        {
            SceneManager.LoadScene("Title"); // Create and change to Try Again scene
        }
    }





}






