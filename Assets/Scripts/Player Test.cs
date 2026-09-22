
using UnityEngine;

public class Player_Test : MonoBehaviour
{
    public float speed = 5f;

    private Animator animator;
    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.5f;

    private bool Dash = false;
    private float dashTimer = 0f;

    [Header("VFX")]
    public GameObject dashVFX;


    void Start()
    {
        dashVFX.SetActive(false);
        animator = GetComponent<Animator>();    
    }

    void Update()
    {
       
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);


        float currentspeed = Dash ? dashSpeed : speed;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Dash = true;
            dashTimer = dashDuration;
            animator.SetBool("Dash", true);

            if (dashVFX != null)
            {
                dashVFX.SetActive(true);
            }
        }
        if(Dash)
        {
            dashTimer -= Time.deltaTime;
            if(dashTimer < 0)
            {
                Dash = false;
                animator.SetBool("Dash", false);
                if (dashVFX != null)
                {
                    dashVFX.SetActive(false);
                }
            }
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

