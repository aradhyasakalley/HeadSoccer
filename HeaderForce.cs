using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeaderForce : MonoBehaviour
{
    public float forceMagnitude = 50f; 
    public float forceDirection;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Football"))
        {
            //Debug.Log("Header !!");

            Rigidbody footballRigidbody = other.GetComponent<Rigidbody>();

            if (footballRigidbody != null)
            {
                if ( forceDirection == 0 )
                {
                    Vector3 forceDirection = transform.right;
                    Debug.Log(forceDirection);
                    footballRigidbody.AddForce(forceDirection * forceMagnitude);
                }   
                else
                {
                    Vector3 forceDirection = transform.right;
                    Debug.Log(forceDirection);
                    footballRigidbody.AddForce(forceDirection * forceMagnitude);
                }
                
            }
        }
    }
}
