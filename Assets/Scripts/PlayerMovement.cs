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


    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
    }

    // Go back to Title Screen if player dies (hits kill zone)
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Kill")){
            SceneManager.LoadScene("Title");
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


    }
}
