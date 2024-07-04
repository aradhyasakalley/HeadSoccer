using System;
using UnityEngine;
using System.Collections;

public class GoalZone : MonoBehaviour
{
    public static event Action<int> OnGoalScored;

    public int side;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Football"))
        {
            Debug.Log("Goal on " + side + "side!!");
            OnGoalScored?.Invoke(side);
        }
    }
}
