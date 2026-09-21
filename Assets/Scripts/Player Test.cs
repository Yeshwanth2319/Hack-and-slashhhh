
using UnityEngine;

public class Player_Test : MonoBehaviour
{
    public float speed = 5f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
       
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);
       

        float currentspeed = speed;
        if (Input.GetKey(KeyCode.Space))
        {
            currentspeed = speed * 4;
            animator.SetBool("Dash", true);
        }
        else
        {
            animator.SetBool("Dash", false);
        }
        transform.Translate(movement * currentspeed * Time.deltaTime, Space.World);

        if (movement.magnitude > 0f)
        {
            animator.SetBool("isWalking", true);
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 720 * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
    }

}

