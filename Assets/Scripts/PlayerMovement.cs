using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private float axis;

    [SerializeField]
    private float speed =1f;

    [SerializeField]
    private float rotationSpeed;
    public int jumpForce = 7;
    public LayerMask whatIsGround;
    public Transform feet;
    bool grounded = false;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Go back to Title Screen if player dies (hits kill zone)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Kill")){
            SceneManager.LoadScene("Title"); // Create and change to Try Again scene
        }
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

        // Use this if you want player to be like kirby/flappy bird with the ability to float around
        if(Input.GetButtonDown("Jump")) //space bar
        {
            rb.AddForce(new Vector2(0, jumpForce));
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
}
