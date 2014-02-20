using UnityEngine;
using System.Collections;

/// <summary>
/// Controller for the gameplay.
/// </summary>
public class GameController : MonoBehaviour 
{
	/// <summary>
	/// The list of hazards in the game that will be selected randomly to annoy
	/// the Player.
	/// </summary>
	public GameObject[] hazards;

	/// <summary>
	/// The x, y and z values to spawn the asteroid. y value will be 0.0f, since
	/// there is no height in the game.
	/// </summary>
	public Vector3 spawnValues;

	/// <summary>
	/// The number of asteroids to spawn in a wave.
	/// </summary>
	public int hazardCount;

	/// <summary>
	/// The number of seconds to wait before spawing the next asteroid in a wave.
	/// </summary>
	public float spawnWait;

	/// <summary>
	/// The number of seconds to wait before starting the asteroid wave.
	/// </summary>
	public float startWait;

	/// <summary>
	/// The number of seconds to wait before starting the next wave of asteroids.
	/// </summary>
	public float waveWait;

	/// <summary>
	/// The GUIText component that is used to display the restart message 
	/// after the game is over.
	/// </summary>
	public GUIText restartText;

	/// <summary>
	/// The GUIText component that is used to display the Game Over message.
	/// </summary>
	public GUIText gameOverText;

	/// <summary>
	/// The GUIText component that is used for displaying the Player's score.
	/// </summary>
	public GUIText playerScoreText;
	
	private int playerScore;
	/// <summary>
	/// The Player's total score.
	/// </summary>
	private int PlayerScore 
	{
		get { return playerScore; }
		set
		{
			playerScore = value;
			playerScoreText.text = string.Format("Score: {0}", playerScore);
		}
	}

	private bool isGameOver;
	/// <summary>
	/// Returns <c>true</c> if game is over; <c>false</c> otherwise.
	/// </summary>
	private bool IsGameOver 
	{
		get { return isGameOver; }
		set
		{
			isGameOver = value;
			gameOverText.text = isGameOver ? "Game Over" : string.Empty;
		}
	}

	private bool shouldRestartGame;
	/// <summary>
	/// Returns <c>true</c> if game should be restarted; <c>false</c> otherwise.
	/// </summary>
	private bool ShouldRestartGame 
	{
		get { return shouldRestartGame; }
		set
		{
			shouldRestartGame = value;
			restartText.text = shouldRestartGame ? "Press 'R' for Restart" : string.Empty;
		}
	}

	void Start()
	{
		IsGameOver = false;
		ShouldRestartGame = false;
		PlayerScore = 0;

		StartCoroutine( SpawnWaves() );
	}

	void Update()
	{
		// Player wants to restart the game after the game is over.
		if (ShouldRestartGame && Input.GetKeyDown(KeyCode.R)) 
		{
			// Reload the current level. There is only one level anyway.
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	IEnumerator SpawnWaves()
	{
		// Give Player some time before we start spawning the waves.
		yield return new WaitForSeconds(startWait);

		// Keep spawning hazards while Player is still alive and kicking.
		while (!IsGameOver) 
		{
			// Spawn the given number of hazards per wave.
			for (int i = 0; i < hazardCount; i++) 
			{
				SpawnHazard();
				yield return new WaitForSeconds(spawnWait);
			}

			// Wait for a while before starting the next wave.
			yield return new WaitForSeconds(waveWait);
		}

		// Player is dead. We should restart the game.
		ShouldRestartGame = true;
	}

	/// <summary>
	/// Spawns a random hazard object (asteroids or enemy ships) at a 
	/// random x-axis position.
	/// </summary>
	void SpawnHazard()
	{
		GameObject hazard = hazards[ Random.Range(0, hazards.Length) ];
		Vector3 spawnPosition = spawnValues;
		spawnPosition.x = Random.Range(-spawnValues.x, spawnValues.x); 
		Instantiate(hazard, spawnPosition, Quaternion.identity);
	}

	/// <summary>
	/// Adds the given points to the Player's total score.
	/// </summary>
	/// <param name="points">The points to add to the Player's total score.</param>
	public void AddScore(int points)
	{
		PlayerScore += points;
	}

	/// <summary>
	/// This method should only be called to end the game.
	/// </summary>
	public void GameOver()
	{
		IsGameOver = true;
	}	
}
