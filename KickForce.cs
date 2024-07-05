using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickForce : MonoBehaviour
{
    public float forceMagnitude = 50f;
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody footballRigidbody = other.GetComponent<Rigidbody>();
        if  ( other.gameObject.CompareTag("Football"))
        {
            Vector3 forceDirection = transform.right;
            footballRigidbody.AddForce(forceDirection * forceMagnitude);
        }
    }
}
