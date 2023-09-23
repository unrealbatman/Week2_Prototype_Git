using UnityEngine;
using UnityEngine.AI;
using System.Collections;


public class Chase : MonoBehaviour
{
    public GameObject target;
    public float jumpForce;
    private NavMeshAgent agent;
    private Rigidbody2D rb;
    private bool trigger;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody2D>();

        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < 0)
        {
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }
        if (trigger == true)
        {
            StartCoroutine(HandleJump());
        }
        else
        {
            agent.SetDestination(target.transform.position);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jump"))
        {
            trigger = true;
        }
        else
        {
            trigger = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        trigger = false;
    }

    IEnumerator HandleJump() {


        yield return new WaitForSeconds(1f);
        rb.AddForce(new Vector2(rb.velocity.x, rb.velocity.y));


    }
}


