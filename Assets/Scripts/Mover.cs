using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
	/// <summary>
	/// The speed of the laser bolt.
	/// </summary>
	public float speed;

	void Start () 
	{
		// When the laser bolt is added to the scene, we give it 
		// a velocity to move it forward.
		rigidbody.velocity = transform.forward * speed;
	}

}
