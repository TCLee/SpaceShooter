using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour 
{
	/// <summary>
	/// The tumble.
	/// </summary>
	public float tumble;
		
	void Start () 
	{
		rigidbody.angularVelocity = Random.insideUnitSphere * tumble;
	}	
}
