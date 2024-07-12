using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject ball;
    private int leftSideGoals = 0;
    private int rightSideGoals = 0;
    private Vector3 goalBallPosition = new Vector3(0.04f, 3f, 90f);
    private Quaternion goalBallRotation = Quaternion.Euler(0f, 90f, 0f);
    private float pauseTime = 1f; // Reduced pause time for half-time
    [SerializeField] TextMeshProUGUI winnerText;
    [SerializeField] TextMeshProUGUI goalText;
    [SerializeField] TextMeshProUGUI halfTimeText;
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
        GameTime.OnGameTimeOver += HandleGameTimeOver;
        GameTime.OnHalfTime += HandleHalfTime;
    }

    private void OnDisable()
    {
        GoalZone.OnGoalScored -= HandleGoalScored;
        GameTime.OnGameTimeOver -= HandleGameTimeOver;
        GameTime.OnHalfTime -= HandleHalfTime;
    }

    private void HandleGoalScored(int side)
    {
        gameTime.PauseTimer();
        StartCoroutine(PauseAndResetPositions());

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
        FreezePlayer(player1);
        FreezePlayer(player2);

        goalText.text = "GOAL !!!";
        goalText.gameObject.SetActive(true);

        Debug.Log("Goal pause time!!");
        yield return new WaitForSeconds(pauseTime);

        goalText.gameObject.SetActive(false);
        // Unfreeze the ball
        if (ballRb != null)
        {
            ballRb.constraints = RigidbodyConstraints.None;
            ResetBallPosition();
        }

        // Unfreeze and constrain the players
        UnfreezePlayer(player1);
        UnfreezePlayer(player2);

        ResetPlayerPositions();

        gameTime.ResumeTimer();
    }

    private void FreezePlayer(PlayerController player)
    {
        if (player != null)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.velocity = Vector3.zero;
                playerRb.angularVelocity = Vector3.zero;
                playerRb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }

    private void UnfreezePlayer(PlayerController player)
    {
        if (player != null)
        {
            Rigidbody playerRb = player.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                playerRb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
            }
        }
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

    private IEnumerator EndingGame()
    {
        Debug.Log("Started coroutine");
        yield return new WaitForSeconds(2f); 
        SceneManager.LoadSceneAsync(0);
    }

    private void HandleGameTimeOver()
    {
        Debug.Log("Time over!");

        // Deactivate the goal text
        goalText.gameObject.SetActive(false);

        if (leftSideGoals > rightSideGoals)
        {
            winnerText.text = "Blue Wins !";
            winnerText.gameObject.SetActive(true);
        }
        else if (rightSideGoals > leftSideGoals)
        {
            winnerText.text = "Red Wins !";
            winnerText.gameObject.SetActive(true);
        }
        else
        {
            winnerText.text = "Draw !";
            winnerText.gameObject.SetActive(true);
        }
        StartCoroutine(EndingGame());
    }

    private void HandleHalfTime()
    {
        Debug.Log("Half-time!");

        // Display half-time text and pause the game
        halfTimeText.text = "HALF TIME !!";
        halfTimeText.gameObject.SetActive(true);

        StartCoroutine(PauseAndResetPositions());

        halfTimeText.gameObject.SetActive(false);
    }

}

