using System;
using UnityEngine;

public class Player_Test : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    public float RunSpeed = 10f;

    [Header("Camera")]
    public Transform cameraTransform;

    private Animator animator;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.5f;
    public bool Dash = false;
    private float dashTimer = 0f;

    [Header("VFX")]
    public GameObject dashVFX;

    [Header("SFX")]
    public AudioSource dashSFX;
    public AudioClip dashSound;
    public AudioClip walkSFX;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (dashVFX != null)
        {
            dashVFX.SetActive(false);
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // CAMERA RELATIVE MOVEMENT
        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movement =
            cameraForward * vertical +
            cameraRight * horizontal;

        movement = Vector3.ClampMagnitude(movement, 1f);

        // RUN
        bool isRunning =
            Input.GetKey(KeyCode.LeftShift) &&
            movement.magnitude > 0.01f;

        // SPEED
        float currentSpeed;

        if (Dash)
        {
            currentSpeed = dashSpeed;
        }
        else if (isRunning)
        {
            currentSpeed = RunSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        // DASH START
        if (Input.GetKeyDown(KeyCode.Space) && !Dash)
        {
            Dash = true;

            dashTimer = dashDuration;

            animator.SetBool("Dash", true);

            if (dashSFX != null && dashSound != null)
            {
                dashSFX.PlayOneShot(dashSound);
            }

            if (dashVFX != null)
            {
                dashVFX.SetActive(true);
            }
        }

        // DASH TIMER
        if (Dash)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
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

        // MOVE
        transform.Translate(
            movement * currentSpeed * Time.deltaTime,
            Space.World
        );

        // ANIMATION + PLAYER ROTATION
        if (movement.magnitude > 0.01f)
        {
            animator.SetBool("isWalking", true);
            animator.SetBool("isRunning", isRunning);

            if (dashSFX != null && walkSFX != null)
            {
                if (!dashSFX.isPlaying)
                {
                    dashSFX.PlayOneShot(walkSFX);
                }
            }

            // PLAYER FACES MOVEMENT DIRECTION
            Quaternion targetRotation =
                Quaternion.LookRotation(movement, Vector3.up);

            transform.rotation =
                Quaternion.RotateTowards(
                    transform.rotation,
                    targetRotation,
                    720f * Time.deltaTime
                );
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetBool("isRunning", false);
        }
    }
}