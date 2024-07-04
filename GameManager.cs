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

    private void OnEnable()
    {
        GoalZone.OnGoalScored += HandleGoalScored;
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
        StartCoroutine(PauseAndResetBall());
    }

    private IEnumerator PauseAndResetBall()
    {
        // Freeze the ball
        Rigidbody ballRb = ball.GetComponent<Rigidbody>();
        ballRb.velocity = Vector3.zero;
        ballRb.angularVelocity = Vector3.zero;
        ballRb.constraints = RigidbodyConstraints.FreezeAll;

        Debug.Log("Goal pause time!!");
        yield return new WaitForSeconds(pauseTime);
        ballRb.constraints = RigidbodyConstraints.None;
        gameTime.ResumeTimer();
        ResetBallPosition();
    }

    private void ResetBallPosition()
    {
        ball.transform.position = goalBallPosition;
        ball.transform.rotation = goalBallRotation;
    }
}
