using UnityEngine;

public class BreakableStone : MonoBehaviour
{
    public GameObject breakEffect;

    [Header("Break Sound")]
    public AudioClip breakSound;

    public CameraFollow cameraFollow;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player_Test player =
                collision.gameObject.GetComponent<Player_Test>();

            if (player != null && player.Dash)
            {
                // Particle
                if (breakEffect != null)
                {
                    Instantiate(
                        breakEffect,
                        transform.position,
                        Quaternion.identity
                    );
                }

                // Camera Shake
                if (cameraFollow != null)
                {
                    cameraFollow.ShakeCamera();
                }   

                // Sound
                if (breakSound != null)
                {
                    AudioSource.PlayClipAtPoint(
                        breakSound,
                        transform.position
                    );
                }

                // Destroy Stone
                Destroy(gameObject);
            }
        }
    }
}