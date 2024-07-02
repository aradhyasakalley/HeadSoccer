using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Header : MonoBehaviour
{
    public float forceAmount = 500f; 

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Football"))
        {
            Rigidbody ballRigidbody = collision.gameObject.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                Vector3 forceDirection = Vector3.right * forceAmount;
                ballRigidbody.AddForce(forceDirection);
            }
        }
    }
}

