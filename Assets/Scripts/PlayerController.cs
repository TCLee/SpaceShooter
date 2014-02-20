using UnityEngine;
using System.Collections;

/// <summary>
/// <c>Boundary</c> represents a rectangle bounds.
/// </summary>
[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	/// <summary>
	/// The speed of the Player's ship. This value will be set from Unity's 
	/// Inspector panel.
	/// </summary>
	public float speed;

	/// <summary>
	/// The boundary of the game world.
	/// </summary>
	public Boundary boundary;
	
	/// <summary>
	/// The tilt of the spaceship when banking to the left or right.
	/// </summary>
	public float tilt;

	/// <summary>
	/// The laser bolt game object that will be created when the 
	/// Player presses 'Fire'.
	/// </summary>
	public GameObject laserBolt;

	/// <summary>
	/// The transform containing the position and rotation where the laser bolt
	/// game object will be created at.
	/// </summary>
	public Transform shotSpawn;

	/// <summary>
	/// Player's firing rate.
	/// </summary>
	public float fireRate = 0.5f;

	/// <summary>
	/// The time in seconds when the Player can fire the next shot.
	/// </summary>
	private float nextFire = 0.0f;

	void Update()
	{
		// Create the laser bolt when Player presses the 'Fire' key.
		// We also limit the number of laser bolts the Player can fire at 
		// any one time.
		if (Input.GetButton("Fire1") && Time.time > nextFire) 
		{
			nextFire = Time.time + fireRate;
			Instantiate(laserBolt, shotSpawn.position, shotSpawn.rotation);

			// Play the sound effect when Player fires the spaceship's weapons.
			audio.Play();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		// Move the spaceship based on player's input.
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		// Clamp the spaceship to be within the game world's boundaries.
		rigidbody.position = new Vector3(
			Mathf.Clamp(rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rigidbody.position.z, boundary.zMin, boundary.zMax)
		);

		// Tilt the spaceship when banking to the left or right.
		rigidbody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}
}