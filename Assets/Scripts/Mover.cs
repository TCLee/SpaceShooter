using UnityEngine;
using System.Collections;

/// <summary>
/// Moves a game object that this script is attached to.
/// </summary>
public class Mover : MonoBehaviour 
{
	/// <summary>
	/// The speed of the game object. Use negative values to invert the direction.
	/// </summary>
	public float speed;

	void Start () 
	{
		// When the laser bolt is added to the scene, we give it 
		// a velocity to move it forward.
		rigidbody.velocity = transform.forward * speed;
	}

}
