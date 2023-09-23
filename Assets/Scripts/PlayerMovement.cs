using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.Universal;


public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;
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

    public int terminalVelocity = -25;
    private Camera cam;
    private AudioSource audioSource;
    public int jumpForce = 7;
    public int jumpForce_max = 14;
    public float lightDuration = 5f;




    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
        flashLight = this.GetComponentInChildren<Light2D>();
        animator = GetComponent<Animator>();
        cam = FindObjectOfType<Camera>();
        //cam.GetComponent<AudioSource>().Play();
        cam.GetComponent<AudioSource>().time = 0.5f;

        audioSource = GetComponent<AudioSource>();
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


        animator.SetInteger("speed", 0);
        if (axis != 0)
        {
            //float rotDir = axis == 1 ? 0 : 180;
            //this.transform.localRotation = Quaternion.Euler(0,rotDir * rotationSpeed,0);
            rb.velocity = new Vector2(axis * speed ,rb.velocity.y);
            if (axis < 0) sr.flipX = true;
            else sr.flipX = false;
            if (isGrounded == true) animator.SetInteger("speed", (int)Mathf.Abs(rb.velocity.x));
        }

        if (Input.GetButtonDown("Jump") )
        {
            rb.AddForce(new Vector2(0, jumpForce));
            if (rb.velocityY > jumpForce_max) rb.velocityY = jumpForce_max;
            animator.SetTrigger("Jump");
            audioSource.Play();
        }


        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, jumpForce));
            isGrounded = false; // Player has jumped, so they're not grounded anymore
            canToggleLight = true; // Player has jumped, so they can now toggle the light
            animator.SetTrigger("Jump");

        }
        if (rb.velocityY < terminalVelocity) rb.velocityY = terminalVelocity;

        if (flashlightTimer > 0) flashlightTimer -= Time.deltaTime; // Decrement the timer
        else flashLight.enabled = false; // Turn off the flashlight when timer reaches 0
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






