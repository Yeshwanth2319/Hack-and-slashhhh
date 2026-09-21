using UnityEngine;
public class CameraFollow : MonoBehaviour
{ 
    public Transform Player; 
    public float smoothSpeed = 10f; 
    private Vector3 offset; 
    void Start() 
    { 
        // Save starting camera offset
        offset = Player.InverseTransformPoint(transform.position); 
    } 
    void LateUpdate() 
    {
        // Follow player with the same relative
        Vector3 targetPosition = Player.TransformPoint(offset);
        transform.position = Vector3.Lerp( transform.position, targetPosition, 
            smoothSpeed * Time.deltaTime ); 
        // Match player's
         Quaternion targetRotation = Player.rotation; 
        transform.rotation = Quaternion.Lerp( transform.rotation, targetRotation, smoothSpeed * Time.deltaTime );
    }
}