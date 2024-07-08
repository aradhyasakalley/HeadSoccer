using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickForce : MonoBehaviour
{
    public float forceMagnitude = 50f;
    public float rotationAngle = 3f; 
    public float resetTime = 0.5f;

    public int player;

    private Quaternion originalRotation;

    private void Start()
    {
        originalRotation =  transform.rotation;
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody footballRigidbody = other.GetComponent<Rigidbody>();
        if  ( other.gameObject.CompareTag("Football"))
        {
            if ( player == 0 )
            {
                Vector3 forceDirection = transform.right * -1;
                footballRigidbody.AddForce(forceDirection * forceMagnitude);
            }
            else
            {
                Vector3 forceDirection = transform.right;
                footballRigidbody.AddForce(forceDirection * forceMagnitude);
            }
            RotateFoot();
            StartCoroutine(ResetRotation());
        }
    }

    private void RotateFoot()
    {
        transform.Rotate(0, 0, rotationAngle);
    }
    IEnumerator ResetRotation()
    {
        yield return new WaitForSeconds(resetTime);
        transform.rotation = originalRotation;
    }
}
