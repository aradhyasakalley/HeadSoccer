using System;
using UnityEngine;

public class GoalZoneLeft : MonoBehaviour
{
    public static event Action<int> OnGoalScored;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Football"))
        {
            Debug.Log("Goal on left side!!");
            OnGoalScored?.Invoke(0);
        }
    }
}
