using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
	/// <summary>
	/// The explosion visual effect to be displayed when an asteroid is 
	/// destroyed by a laser bolt.
	/// </summary>
	public GameObject explosion;

	/// <summary>
	/// The explosion visual effect for the Player object when it rams 
	/// into an asteroid.
	/// </summary>
	public GameObject playerExplosion;

	/// <summary>
	/// The number of points the Player will score when the asteroid is destroyed.
	/// </summary>
	public int scoreValue;

	/// <summary>
	/// The reference to the single GameController instance that controls 
	/// the gameplay.
	/// </summary>
	private GameController gameController;

	void Start()
	{
		GameObject gameObject = GameObject.FindWithTag("GameController");

		if (gameObject) 
		{
			gameController = gameObject.GetComponent<GameController>();
		} 
		else 
		{
			// If this case ever happens, it is considered a developer error.
			Debug.Log("Cannot find game object with tag 'GameController'.");
		}
	}

	void OnTriggerEnter(Collider other)
	{
		// Ignore the Boundary object. Otherwise, the asteroids will be
		// destroyed as soon as the game starts.
		if (other.tag == "Boundary") { return; }

		// Player's spaceship rams onto the asteroid.
		if (other.tag == "Player") 
		{
			// Create an explosion VFX for the Player's spaceship collision.
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

			// Player is dead. Tell the Game Controller to end the game.
			gameController.GameOver();
		}

		// Create the explosion visual effect at the asteroid's location.
		Instantiate(explosion, transform.position, transform.rotation);

		// Destroy the laser bolt that hit the asteroid.
		Destroy(other.gameObject);

		// Destroy the asteroid too.
		// This script is attached to the asteroid object.
		Destroy(gameObject);

		// Add to Player' total score when asteroid is successfully shot down.
		gameController.AddScore(scoreValue);
	}
}
