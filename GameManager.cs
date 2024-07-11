using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    private int leftSideGoals = 0;
    private int rightSideGoals = 0;
    private Vector3 goalBallPosition = new Vector3(0.04f, 3f, 90f);
    private Quaternion goalBallRotation = Quaternion.Euler(0f, 90f, 0f);
    private float pauseTime = 2f;

    public TMP_Text score_left;
    public TMP_Text score_right;

    public GameTime gameTime;

    public PlayerController player1;
    public PlayerController player2;
    private Vector3 player1StartPosition;
    private Quaternion player1StartRotation;
    private Vector3 player2StartPosition;
    private Quaternion player2StartRotation;

    private void Start()
    {
        if (player1 != null)
        {
            player1StartPosition = player1.transform.position;
            player1StartRotation = player1.transform.rotation;
        }
        else
        {
            Debug.LogError("Player1 is not assigned in the GameManager.");
        }

        if (player2 != null)
        {
            player2StartPosition = player2.transform.position;
            player2StartRotation = player2.transform.rotation;
        }
        else
        {
            Debug.LogError("Player2 is not assigned in the GameManager.");
        }
    }

    private void OnEnable()
    {
        GoalZone.OnGoalScored += HandleGoalScored;
    }

    private void OnDisable()
    {
        GoalZone.OnGoalScored -= HandleGoalScored;
    }

    private void HandleGoalScored(int side)
    {
        if (side == 0)
        {
            leftSideGoals++;
            score_left.text = leftSideGoals.ToString();
            Debug.Log($"Left Side Goals: {leftSideGoals}, Right Side Goals: {rightSideGoals}");
        }
        else if (side == 1)
        {
            rightSideGoals++;
            score_right.text = rightSideGoals.ToString();
            Debug.Log($"Left Side Goals: {leftSideGoals}, Right Side Goals: {rightSideGoals}");
        }
        gameTime.PauseTimer();
        StartCoroutine(PauseAndResetPositions());
    }

    private IEnumerator PauseAndResetPositions()
    {
        // Freeze the ball
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        if (ballRb != null)
        {
            ballRb.velocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
            ballRb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            Debug.LogError("Ball Rigidbody is not assigned or found.");
        }

        // Freeze the players
        if (player1 != null)
        {
            Rigidbody player1Rb = player1.GetComponent<Rigidbody>();
            if (player1Rb != null)
            {
                player1Rb.velocity = Vector3.zero;
                player1Rb.angularVelocity = Vector3.zero;
                player1Rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        if (player2 != null)
        {
            Rigidbody player2Rb = player2.GetComponent<Rigidbody>();
            if (player2Rb != null)
            {
                player2Rb.velocity = Vector3.zero;
                player2Rb.angularVelocity = Vector3.zero;
                player2Rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        Debug.Log("Goal pause time!!");
        yield return new WaitForSeconds(pauseTime);

        // Unfreeze the ball
        if (ballRb != null)
        {
            ballRb.constraints = RigidbodyConstraints.None;
            ResetBallPosition();
        }

        // Unfreeze and constrain the players
        if (player1 != null)
        {
            Rigidbody player1Rb = player1.GetComponent<Rigidbody>();
            if (player1Rb != null)
            {
                player1Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            }
        }

        if (player2 != null)
        {
            Rigidbody player2Rb = player2.GetComponent<Rigidbody>();
            if (player2Rb != null)
            {
                player2Rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            }
        }

        ResetPlayerPositions();

        gameTime.ResumeTimer();
    }

    private void ResetBallPosition()
    {
        ball.transform.position = goalBallPosition;
        ball.transform.rotation = goalBallRotation;
    }

    private void ResetPlayerPositions()
    {
        if (player1 != null)
        {
            player1.transform.position = player1StartPosition;
            player1.transform.rotation = player1StartRotation;
        }

        if (player2 != null)
        {
            player2.transform.position = player2StartPosition;
            player2.transform.rotation = player2StartRotation;
        }
    }
}
