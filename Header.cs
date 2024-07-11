using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Header : MonoBehaviour
{
    public float forceMagnitude = 50.0f;

    private void OnTriggerEnter(Collider collision)
    {
        Rigidbody footballRigidbody = collision.gameObject.GetComponent<Rigidbody>();
        if (footballRigidbody != null && collision.gameObject.CompareTag("Football"))
        {
            Debug.Log("HEADERRRRR!!!!");
            Vector3 forceDirection = transform.right;
            footballRigidbody.AddForce(forceDirection * forceMagnitude);

            /*Debug.Log("Force Direction: " + forceDirection);
            Debug.Log("Force Magnitude: " + forceMagnitude);
            Debug.Log("Applied Force: " + forceDirection * forceMagnitude);*/
        }
    }
}

