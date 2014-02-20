using UnityEngine;
using System.Collections;

/// <summary>
/// Destroy the game objects when they collide into one another.
/// Example: Player ramming into an asteroid. Player's laser bolt shooting down an 
/// enemy ship.
/// </summary>
public class DestroyByContact : MonoBehaviour 
{
	/// <summary>
	/// The explosion visual effect to be displayed when the game objects collide.
	/// This is optional. Not all game objects need an explosion visual effect.
	/// </summary>
	public GameObject explosion;

	/// <summary>
	/// The explosion visual effect for the Player object when it collides 
	/// into an enemy.
	/// </summary>
	public GameObject playerExplosion;

	/// <summary>
	/// The number of points the Player will score when the enemy is destroyed.
	/// </summary>
	public int scoreValue;

	/// <summary>
	/// The reference to the single GameController instance that controls 
	/// the gameplay.
	/// </summary>
	private GameController gameController;

	void Start()
	{
		// Attempt to find the first and only Game Controller object in the scene.
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
		// Ignore the Boundary object. Otherwise, the hazards will be
		// destroyed as soon as the game starts.
		// Ignore "Enemy" tags. When the enemy spaceship fires a laser bolt, 
		// the laser bolt will be spawned near the enemy spaceship and this will
		// blow up the enemy spaceship.
		if (other.tag == "Boundary" || other.tag == "Enemy") 
		{ 
			return; 
		}

		// Create the optional explosion visual effect.
		if (explosion) 
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		// Player's spaceship rams onto the asteroid or enemy spaceship.
		if (other.tag == "Player") 
		{
			// Create an explosion VFX for the Player's spaceship collision.
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);

			// Player is dead. Tell the Game Controller to end the game.
			gameController.GameOver();
		}

		// Add to Player' total score when enemy or asteroid is successfully shot down.
		gameController.AddScore(scoreValue);

		// Destroy both the game objects that collided into one another.
		Destroy(other.gameObject);
		Destroy(gameObject);
	}
}
