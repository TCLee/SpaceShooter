using UnityEngine;
using System.Collections;

/// <summary>
/// Fires the weapon repeatedly based on the weapon's fire rate settings.
/// </summary>
public class WeaponController : MonoBehaviour 
{
	/// <summary>
	/// The game object that represents the laser bolt being fired.
	/// </summary>
	public GameObject shot;

	/// <summary>
	/// The position and rotation transform where the shot should be spawned from.
	/// </summary>
	public Transform shotSpawn;

	/// <summary>
	/// The fire rate of the weapon in seconds.
	/// </summary>
	public float fireRate;

	/// <summary>
	/// The delay in seconds before firing the first shot.
	/// </summary>
	public float delay;
	
	void Start () 
	{
		InvokeRepeating("FireWeapon", delay, fireRate);
	}

	/// <summary>
	/// This method will be called repeatedly by <c>InvokeRepeating</c>.
	/// </summary>
	private void FireWeapon()
	{
		// Create the shot (laser bolt) game object.
		Instantiate(shot, shotSpawn.position, shotSpawn.rotation);

		// Play the weapon's sound effects.
		audio.Play();
	}
}
