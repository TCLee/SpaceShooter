using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour 
{
	/// <summary>
	/// The number of seconds to wait before destroying this game object.
	/// </summary>
	public float lifetime;

	// Use this for initialization
	void Start () 
	{
		Destroy(gameObject, lifetime);
	}		
}
