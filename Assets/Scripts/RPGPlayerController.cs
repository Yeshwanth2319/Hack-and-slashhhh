
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;

    //private Animator animator;
   
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Get keyboard input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Create movement direction
        Vector3 movement = new Vector3(horizontal,0f,vertical);
        transform.Translate(movement * speed * Time.deltaTime,Space.World);

        //float movementAmount = movement.magnitude;
       // animator.SetFloat("Speed", movementAmount);
    }
   
}

