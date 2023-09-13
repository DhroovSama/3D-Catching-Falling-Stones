using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameSceneManager : MonoBehaviour
{
	private float BALL_INTERVAL_MAX = 2.0f;
	private float BALL_INTERVAL_MIN = 0.2f;
	private float TIME_TO_MINIMUM_INTERVAL = 30.0f;	
	public float GAME_TIME = 45.0f;

	public Camera mainCamera;
	public Text scoreText;
	public Text gameOverText;
	public PlayerController player;
	public GameObject ballPrefab;

	private Vector3 leftBound;
	private int score;
	private float gameTimer;
	private float ballTimer;
	private bool gameOver;

	public void Start ()
	{
        
		Time.timeScale = 1;

		ballTimer = BALL_INTERVAL_MAX;

		leftBound = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, -mainCamera.transform.localPosition.z));

		player.OnCollectBall += OnCollectBall;

        scoreText.enabled = true;
        gameOverText.enabled = false;
	}

	public void Update ()
	{
        
		if (gameOver)
		{
			if (Input.GetKeyDown("r"))
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().name);
			}

			scoreText.enabled = false;
			gameOverText.enabled = true;

			gameOverText.text = "Game over! Total score: " + score + "\nPress R to restart";

			return;
		}

		scoreText.text = "Score: " + score + "\nTime left: " + Mathf.Floor(Mathf.Max((GAME_TIME - gameTimer), 0));

		gameTimer += Time.deltaTime;
		ballTimer -= Time.deltaTime;
		if (ballTimer <= 0)
		{
		    float intervalPercentage = Mathf.Min(gameTimer / TIME_TO_MINIMUM_INTERVAL, 1);
		    ballTimer = BALL_INTERVAL_MAX - (BALL_INTERVAL_MAX - BALL_INTERVAL_MIN) * intervalPercentage;

		    GameObject ball = GameObject.Instantiate<GameObject>(ballPrefab);
		    ball.transform.SetParent(this.transform);
		    ball.transform.position = new Vector3
		    (
		    	Random.Range(-5, 5),
		    	leftBound.y + 2,
		    	0
		    );
		}

		if (gameTimer > GAME_TIME)
		{
			OnGameOver();
		}
	}

	public void OnCollectBall ()
	{
		if (!gameOver)
		{
			score += 100;
		}
	}

	public void OnGameOver ()
	{
		gameOver = true;

		Time.timeScale = 0;
	}
}